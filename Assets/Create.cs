using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class Create : MonoBehaviourPunCallbacks
{
    [SerializeField]InputField _roomName;
    [SerializeField] GameObject _titleCanvas;

    public void ServerLogin()
    {
        _titleCanvas.SetActive(false);
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
    }

    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    //public override void OnJoinedRoom()
    //{
    //    // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
    //    var position = new Vector3(Random.Range(-5, 5), 3,  Random.Range(-5, 5));
    //    PhotonNetwork.Instantiate("Avatar", position, Quaternion.identity);
    //}
    public override void OnPlayerLeftRoom(Player newplayer)
    {
        Debug.Log($"Player({newplayer.ActorNumber})がログインしました");
    }
}