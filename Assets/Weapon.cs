using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Weapon : MonoBehaviour
{
    [SerializeField] bool _ishotGun;
    [SerializeField] int _attack;
    [SerializeField] int _range;
    [SerializeField] int _intarval;
    [SerializeField] int _bulletCount;

    public void GunAction()
    {
        if (_ishotGun)
        {
            ShotGun(_range, _intarval, _bulletCount);
        }
        else
        {
            Gun(_range, _intarval, _bulletCount);
        }
    }

    public void Gun(int Range, int Intarval, int BulletCount)
    {

    }

    public void ShotGun(int Range, int Intarval, int BulletCount)
    {

    }
}