using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class SceneMove : MonoBehaviourPunCallbacks
{
    public void ScneMoveButton(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}