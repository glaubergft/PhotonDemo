using Photon.Pun;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private PhotonView view;
    private TextMesh nicknameLabel;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddNicknameLabel();
        RetrieveInitialColor();
    }

    private void RetrieveInitialColor()
    {
        string colorName = view.Owner.CustomProperties["ColorName"].ToString();
        UpdateColor(colorName);
    }

    private void AddNicknameLabel()
    {
        GameObject nicknameGameObject = new GameObject("NicknameLabel");
        nicknameLabel = nicknameGameObject.AddComponent<TextMesh>();
        nicknameLabel.text = view.Owner.NickName;
        nicknameLabel.color = Color.grey;
        nicknameLabel.fontStyle = FontStyle.Bold;
        nicknameLabel.alignment = TextAlignment.Center;
        nicknameLabel.anchor = TextAnchor.MiddleCenter;
        nicknameLabel.fontSize = 120;
        nicknameLabel.characterSize = 0.065f;
        nicknameGameObject.transform.parent = transform;
        nicknameGameObject.transform.localPosition = new Vector3(0, 1.5f, 0);
    }

    [PunRPC]
    internal void UpdateNickname(string nickname)
    {
        view.Owner.NickName = GameObject.Find("InputField_Nickname").GetComponent<InputField>().text;
        nicknameLabel.text = nickname;

        if (view.IsMine)
        {
            view.RPC("UpdateNickname", RpcTarget.Others, nickname);
        }
    }

    [PunRPC]
    internal void UpdateColor(string colorName)
    {
        Color color;
        ColorUtility.TryParseHtmlString(colorName, out color);
        gameObject.GetComponent<Renderer>().material.color = color;

        if (view.IsMine)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["ColorName"] = colorName;
            PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
            view.RPC("UpdateColor", RpcTarget.Others, colorName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
