using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Create : MonoBehaviourPunCallbacks
{
    [SerializeField]InputField _roomName;
    [SerializeField] GameObject _titleCanvas;

    public void ServerLogin()
    {
        _titleCanvas.SetActive(false);
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    //public override void OnJoinedRoom()
    //{
    //    // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
    //    var position = new Vector3(Random.Range(-5, 5), 3,  Random.Range(-5, 5));
    //    PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    //}
    public override void OnPlayerLeftRoom(Player newplayer)
    {
        Debug.Log($"Player({newplayer.ActorNumber})�����O�C�����܂���");
    }
}