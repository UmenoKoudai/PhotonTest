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
    [SerializeField] GameObject[] _player;
    [SerializeField] GameObject _roomStage;
    [SerializeField] GameObject _battleStage;
    [SerializeField] Text _countDown;
    [SerializeField] Text _inRoomPlayerCount;
    [SerializeField]int _maxPlayer;

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
        //_open.SetActive(true);
        _close.SetActive(false);
        _inRoomPlayerCount.gameObject.SetActive(true);
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
        Vector3 playerPosition = new Vector3(Random.Range(-3, 3), 5, Random.Range(-3, 3));
        Vector3 roomStagePosition = new Vector3(0, 10, 0);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.Instantiate(_roomStage.name, roomStagePosition, Quaternion.identity);
            PhotonNetwork.Instantiate(_player[0].name, playerPosition, Quaternion.identity);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.Instantiate(_player[1].name, playerPosition, Quaternion.identity);
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            PhotonNetwork.Instantiate(_player[2].name, playerPosition, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(_player[3].name, playerPosition, Quaternion.identity);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
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
                //Vector3 battlestagePosition = new Vector3(0, 60, 0);
                //PhotonNetwork.Instantiate(_battleStage.name, battlestagePosition, Quaternion.identity);
                //Destroy(GameObject.Find(_roomStage.name));
                StartCoroutine(SceneMove());
            }
        }
    }

    IEnumerator SceneMove()
    {
        _countDown.gameObject.SetActive(true);
        _countDown.text = $"{(5 - Time.deltaTime).ToString("d0")}";
        Vector3 battlestagePosition = new Vector3(0, 60, 0);
        PhotonNetwork.Instantiate(_battleStage.name, battlestagePosition, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(GameObject.Find(_roomStage.name));
    }
}