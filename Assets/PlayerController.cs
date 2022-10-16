using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] int _moveSpeed;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = Vector3.forward * v + Vector3.right * h;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if(dir != Vector3.zero)
        {
            transform.right = dir;
        }
        _rb.velocity = dir.normalized * _moveSpeed + _rb.velocity.y * Vector3.up;
    }
}