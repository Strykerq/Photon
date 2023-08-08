using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CubeMove : MonoBehaviour, IPunObservable
{

    private float _speed = 6f;
    private bool _isMoving;
    private float _leftPoint = -30f;
    private float _rightpoint = 30f;
    private PhotonView _photon;
    private Vector3 _currentpostion;

    // Start is called before the first frame update
    private void Start()
    {
        _photon = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_currentpostion);
        }
        else
        {
            _currentpostion = (Vector3)stream.ReceiveNext();
        }
    }

   
    private void Update()
    {          
           if(_photon.IsMine)
           {
              _currentpostion = transform.position;
           }
            if (_isMoving)
            {
                transform.Translate(Vector2.right * _speed * Time.deltaTime);
            }
            else if (!_isMoving)
            {
                transform.Translate(Vector2.left * _speed * Time.deltaTime);
            }

            if (transform.position.x < _leftPoint)
            {
                _isMoving = true;
            }
            else if (transform.position.x > _rightpoint)
            {
                _isMoving = false;
            }

            if(!_photon.IsMine)
            {
               transform.position = _currentpostion;
            }                      
    }    
}
