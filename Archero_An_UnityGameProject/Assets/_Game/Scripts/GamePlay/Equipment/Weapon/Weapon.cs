using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : GameUnit
{
    
    //Bullet
    [Header("-------------Bullet-------------")]
    [SerializeField] BulletData bulletData;
    public Transform bulletPoint;
    public Bullet currentBullet;
    //public Transform bulletHolder;
    public BulletType bulletType;

    public void ChangeBullet(WeaponType weaponType)
    {
        currentBullet = bulletData.GetBullet((int)weaponType);
    }
    public void Fire(Vector3 direct)
    {
        //Bullet bullet = Instantiate(currentBullet, bulletPoint.position, bulletPoint.rotation);
        //bullet.OnInit(direct);
        Bullet bullet = SimplePool.Spawn<Bullet>(currentBullet, bulletPoint.position, bulletPoint.rotation);//.OnInit(direct);
        bullet.OnInit(direct);                                                                   
    }
}
