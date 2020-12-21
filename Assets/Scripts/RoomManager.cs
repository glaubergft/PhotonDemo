using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    private void Connect()
    {
        Debug.Log("Connection");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (!PhotonNetwork.InRoom)
        {
            Debug.Log("Joined Lobby");
            SetupMyPlayer();
            var joinRoomParams = new OpJoinRandomRoomParams();
            var createRoomParams = new EnterRoomParams();
            PhotonNetwork.NetworkingClient.OpJoinRandomOrCreateRoom(joinRoomParams, createRoomParams);
        }
    }

    private void SetupMyPlayer()
    {
        string nickname = GameObject.Find("InputField_Nickname").GetComponent<InputField>().text;
        Dropdown ddColor = GameObject.Find("Dropdown_Color").GetComponent<Dropdown>();
        string colorName = ddColor.options[ddColor.value].text;
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.LocalPlayer.CustomProperties["ColorName"] = colorName;
    }

    public override void OnJoinedRoom()
    {
        SpawnMyPlayer();
    }

    private void SpawnMyPlayer()
    {
        var player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        player.GetComponent<CharacterMovement>().AttachCamera();
        int rndX = UnityEngine.Random.Range(-5, 5);
        int rndZ = UnityEngine.Random.Range(-5, 5);
        player.transform.position = new Vector3(rndX, 0, rndZ);
        player.tag = "Player";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
