using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _open;
    [SerializeField] GameObject _close;
    [SerializeField] InputField _roomName;
    [SerializeField] GameObject _Player;
    [SerializeField] Text _countDown;
    [SerializeField] Text _inRoomPlayerCount;
    int _maxPlayer = 2;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetActive()
    {
        _open.SetActive(true);
        _inRoomPlayerCount.gameObject.SetActive(true);
        _close.SetActive(false);
    }

    public void InRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("ルーム作成中");
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("マスターサーバーにログインしました。");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("ルームに入場しました");
        _open.SetActive(false);
        _inRoomPlayerCount.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{_maxPlayer}";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由でログインできませんでした");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == _maxPlayer)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.IsMessageQueueRunning = false;
                SceneManager.LoadSceneAsync("TestGame", LoadSceneMode.Single);
                //StartCoroutine(SceneMove());
            }
        }
    }

    //IEnumerator SceneMove()
    //{
    //    _countDown.gameObject.SetActive(true);
    //    _countDown.text = $"{(5 - Time.deltaTime).ToString("d0")}";
    //    yield return new WaitForSeconds(5);
    //    SceneManager.LoadSceneAsync("TestGame", LoadSceneMode.Single);
    //}
}