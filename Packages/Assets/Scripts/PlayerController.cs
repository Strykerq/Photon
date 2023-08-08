using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour,IPunObservable
{
    private PhotonView _photonView;
    private MeshRenderer _meshRender;  
    private int _changeColor = 0;
    [SerializeField] private Color [] colors;
    private Quaternion _quaternion;   
                
    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _meshRender = GetComponent<MeshRenderer>();       
    }  
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(_changeColor);
            stream.SendNext(_quaternion);                                
        }
        else
        {
            _changeColor = (int) stream.ReceiveNext();                   
             SyncColor();                                                  
            _quaternion = (Quaternion)stream.ReceiveNext();          
        }
    }
  
    public void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Rotate(0f,5f,0f);
        }
        if (_photonView.IsMine)
        {
            _quaternion = transform.rotation;         

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-Time.deltaTime * 10f, 0f, 0f);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-Time.deltaTime * -10f, 0f, 0f);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _changeColor++;
                if (_changeColor == colors.Length)
                {
                    _changeColor = 0;
                }              
                SyncColor();           
            }         
        }
        if (!_photonView.IsMine)
        {
            transform.rotation = _quaternion;
        }            
    }

    private void SyncColor()
    {
        _meshRender.material.color = colors[_changeColor];
        
    }
}
