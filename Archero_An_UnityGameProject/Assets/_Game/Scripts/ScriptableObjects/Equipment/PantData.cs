using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "PantData")]
public class PantData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetMat(PantType pantType)
    {
        return materials[(int)pantType];
    }
}
