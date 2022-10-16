using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

namespace RGame.Photon
{
    public class CreateRoom : MonoBehaviourPunCallbacks
    {
        const string roomName = "Room";

        [SerializeField] bool useLobby;
        [SerializeField] GameObject lobbyCanvas;
        [SerializeField] GameObject mainCanvas;

        bool isRestarting;

        void Start()
        {
            
        }

        void Update()
        {

        }
    }
}