using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text LogText;
    [SerializeField] private InputField _roomName;
    [SerializeField] private InputField _passwordInput;
    private string _roomPassword = "1234";
    private string _enteredPassword;
    Photon.Realtime.RoomOptions roomOptions;
    

    private void Start()
    {      
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1.0.0";
        PhotonNetwork.ConnectUsingSettings();
        Log("Room name : room");
    }

    private void Update()
    {
        _enteredPassword = _passwordInput.text;
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master"); 
    }

    public void CreateRoom()
    {
        string roomName = "room";
        PhotonNetwork.CreateRoom("room", roomOptions);
        {
            //roomOptions.IsOpen = true;
            //roomOptions.IsVisible = false;                               
        }
        Log(roomName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room, Roomname is : " + PhotonNetwork.CurrentRoom.Name);        
    }  

    public void JoinToRoomWithPassword()
    {
       if(_enteredPassword == _roomPassword)
        {
            PhotonNetwork.JoinRoom("room");
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Log(message);
        Debug.Log(message);
    }

    public override void OnJoinedRoom()
    {
        Log("Joined the room");
        PhotonNetwork.LoadLevel("GameScene");
    }
    public void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
