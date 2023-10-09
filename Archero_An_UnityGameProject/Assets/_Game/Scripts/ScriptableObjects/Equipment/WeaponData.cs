using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] Weapon[] weapons;

    public Weapon GetWeapon(WeaponType weaponType)
    {
        return weapons[(int)weaponType];
    }
}
