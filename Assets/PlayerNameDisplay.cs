using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;
using TMPro;

public class PlayerNameDisplay : MonoBehaviourPunCallbacks
{
    void Start()
    {
        var _playerName = GetComponent<TextMeshPro>();
        _playerName.text = $"{photonView.Owner.NickName}{(photonView.OwnerActorNr)}";
    }
}