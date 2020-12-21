using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float characterSpeed = 8;

    [SerializeField]
    private float jumpSpeed = 12;

    [SerializeField]
    private float gravity = 20;

    private CharacterController characterController;
    private Vector3 inputVec = new Vector3();
    private PhotonView view;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        view = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (view.IsMine || !PhotonNetwork.InRoom)
        {
            AttachCamera();
        }
    }

    internal void AttachCamera()
    {
        GameObject.Find("Main Camera").transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionByInput();
    }

    private void UpdatePositionByInput()
    {
        if (!view.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        inputVec.x = Input.GetAxis("Horizontal") * characterSpeed;
        inputVec.z = Input.GetAxis("Vertical") * characterSpeed;

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            inputVec.y = jumpSpeed;
        }

        inputVec.y -= gravity * Time.deltaTime;
        characterController.Move(inputVec * Time.deltaTime);
    }
}
