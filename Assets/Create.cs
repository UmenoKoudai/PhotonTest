using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAPUN�̃R�[���o�b�N���󂯎���悤�ɂ���
public class Create : MonoBehaviourPunCallbacks
{
    [SerializeField] BoxCollider _stage;
    BoxCollider _bc;
    private void Start()
    {
        _bc = _stage.GetComponent<BoxCollider>();
        // PhotonServerSettings�̐ݒ���e���g���ă}�X�^�[�T�[�o�[�֐ڑ�����
        PhotonNetwork.ConnectUsingSettings();
    }

    // �}�X�^�[�T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnConnectedToMaster()
    {
        // "Room"�Ƃ������O�̃��[���ɎQ������i���[�������݂��Ȃ���΍쐬���ĎQ������j
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // �Q�[���T�[�o�[�ւ̐ڑ��������������ɌĂ΂��R�[���o�b�N
    public override void OnJoinedRoom()
    {
        // �����_���ȍ��W�Ɏ��g�̃A�o�^�[�i�l�b�g���[�N�I�u�W�F�N�g�j�𐶐�����
        var position = new Vector3(Random.Range(-(_bc.size.x)/2, (_bc.size.x)/2), Random.Range(-(_bc.size.z) / 2, (_bc.size.z) / 2));
        PhotonNetwork.Instantiate("Player1", position, Quaternion.identity);
        PhotonNetwork.Instantiate("Player2", position, Quaternion.identity);
    }
}