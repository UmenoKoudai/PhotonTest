using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Realtime;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] int _moveSpeed;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector3 dir = Vector3.forward * v + Vector3.right * h;
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;
            //if (dir != Vector3.zero)
            //{
            //    transform.Rotate(h * 2f, 0, 0);
            //}
            transform.Rotate(0, h * 0.5f, 0);
            _rb.velocity = dir.normalized * _moveSpeed + _rb.velocity.y * Vector3.up;
        }
    }
}