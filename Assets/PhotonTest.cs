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
    [SerializeField] GameObject _countDown;
    [SerializeField] Text _inRoomPlayerCount;
    Text _countDownText;
    int _maxPlayer = 2;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        _countDownText = _countDown.GetComponent<Text>();
    }

    public void SetActive()
    {
        _open.SetActive(true);
        _close.SetActive(false);
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
        Vector3 position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        PhotonNetwork.Instantiate($"{_Player.name}", position, Quaternion.identity);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}の理由でログインできませんでした");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _inRoomPlayerCount.text = $"{PhotonNetwork.CurrentRoom.PlayerCount}/{_maxPlayer}";
        //if (PhotonNetwork.CurrentRoom.PlayerCount == _maxPlayer)
        //{
        //    PhotonNetwork.CurrentRoom.IsOpen = false;
        //    StartCoroutine(SceneMove());
    
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == _maxPlayer)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                StartCoroutine(SceneMove());
            }
        }
    }

    IEnumerator SceneMove()
    {
        _countDown.SetActive(true);
        _countDownText.text = $"{(5 - Time.deltaTime).ToString("d0")}";
        yield return new WaitForSeconds(5);
        PhotonNetwork.LoadLevel("TestGame");
    }
}