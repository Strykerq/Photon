using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _playerPrefab;      
    [SerializeField] private InputField _messageInput;
    [SerializeField] private Text _messageText;
    private PhotonView _photonView;

    private void Start()
    {
       Vector3 positon = new Vector3(0f, 0f, 90f);
       PhotonNetwork.Instantiate(_playerPrefab.name, positon, Quaternion.identity); 
       _photonView = GetComponent<PhotonView>();       
    }

    public void SendButton()
    {
        _photonView.RPC("SendData", RpcTarget.All, _messageInput.text);
    }

    [PunRPC]
    private void SendData(string message)
    {
        _messageText.text = message;
    }
    public void Exit()
    {
       PhotonNetwork.LeaveRoom();
    }   
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Player {0} entered room", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("Player {0} left room", otherPlayer.NickName);
    }
}
