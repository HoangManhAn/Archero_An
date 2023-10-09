using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "BulletData")]
public class BulletData : ScriptableObject
{
    [SerializeField] Bullet[] bullets;

    //public Bullet GetBullet(BulletType bulletType)
    //{
    //    return bullets[(int)bulletType];
    //}

    public Bullet GetBullet(int index)
    {
        return bullets[index];
    }
}
