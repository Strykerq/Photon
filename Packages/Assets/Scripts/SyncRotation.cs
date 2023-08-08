using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    private Quaternion _currentRotation;
    private PhotonView _photonView;
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Rotate(Vector3.up, 8f);
                _currentRotation = transform.rotation;
            }
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            
            stream.SendNext(_currentRotation);
        }
        else
        {
            
            _currentRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
