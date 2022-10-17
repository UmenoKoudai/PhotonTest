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
            if(mainCanvas)
            {
                mainCanvas.SetActive(false);
            }
            if(!PhotonNetwork.IsConnected)
            {
                if(lobbyCanvas)
                {
                    lobbyCanvas.SetActive(false);
                }
                var ownerPlayerName = System.Guid.NewGuid().ToString().Substring(0, 13);
                PhotonNetwork.LocalPlayer.NickName = ownerPlayerName;
                PhotonNetwork.ConnectUsingSettings();
            }
            else if(PhotonNetwork.InLobby)
            {
                Debug.Log($"Start:InLobby");
                lobbyCanvas.SetActive(true);
            }
            else
            {
                ProperJoin();
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log($"connect to master");
            if(isRestarting)
            {
                return;
            }
            ProperJoin();
        }
        void ProperJoin()
        {
            Debug.Log($"proper join");
            if(useLobby && lobbyCanvas)
            {
                PhotonNetwork.JoinLobby();
            }
            else
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
            }
        }
        public override void OnJoinedLobby()
        {
            Debug.Log($"joined Lobby");
            lobbyCanvas.SetActive(true);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"joined room");
            if(lobbyCanvas)
            {
                lobbyCanvas.SetActive(false);
            }
            if(mainCanvas)
            {
                mainCanvas.SetActive(true);
            }
            //FindObjectOfType<GameProperties>().Reset(PhotonNetwork.LocalPlayer);
            PhotonNetwork.Instantiate("PlayerVersionA", Vector3.zero, Quaternion.identity);
        }

        public override void OnLeftRoom()
        {
            Debug.Log($"left room");
            StartCoroutine(RestartScene());
        }

        IEnumerator RestartScene()
        {
            isRestarting = true;

            yield return new WaitForSeconds(1);
            if(!SceneLoadable())
            {
                yield break;
            }
            Debug.Log($"RestartScene");
            SceneManager.LoadScene("Main");
        }
        public bool SceneLoadable()
        {
#if UNITY_EDTTOR
            if(!UnityEditor.EditorApplication.isPlaying)
            {
                return false;
            }
#endif
            return Application.isPlaying;
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log($"join {newPlayer.NickName}");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log($"exit {otherPlayer.NickName}");
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"created room");
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"create room failed {message}");
        }

        void Update()
        {

        }
    }
}