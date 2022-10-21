using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class PlayerCreate : MonoBehaviourPunCallbacks
{
    void Start()
    {
        var position = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5));
        PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    }
}