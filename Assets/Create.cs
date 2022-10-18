using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Create : MonoBehaviourPunCallbacks
{
    [SerializeField] BoxCollider _stage;
    BoxCollider _bc;
    private void Start()
    {
        _bc = _stage.GetComponent<BoxCollider>();
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnJoinedRoom()
    {
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = new Vector3(Random.Range(-(_bc.size.x)/2, (_bc.size.x)/2), Random.Range(-(_bc.size.z) / 2, (_bc.size.z) / 2));
        PhotonNetwork.Instantiate("Player1", position, Quaternion.identity);
        PhotonNetwork.Instantiate("Player2", position, Quaternion.identity);
    }
}