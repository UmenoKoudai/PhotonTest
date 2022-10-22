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

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetActive()
    {
        _open.SetActive(true);
        _close.SetActive(false);
    }

    public void CreateRoom()
    {
        Debug.Log("���[���쐬��");
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, new RoomOptions(), TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�}�X�^�[�T�[�o�[�Ƀ��O�C�����܂����B");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("���[���ɓ��ꂵ�܂���");
        _open.SetActive(false);
        Vector3 position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        PhotonNetwork.Instantiate($"{_Player.name}", position, Quaternion.identity);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"{cause}�̗��R�Ń��O�C���ł��܂���ł���");
    }
}