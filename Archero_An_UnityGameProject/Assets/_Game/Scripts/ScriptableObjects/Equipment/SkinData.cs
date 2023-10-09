using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "SkinData")]
public class SkinData : ScriptableObject
{
    [SerializeField] Material[] materials;

    public Material GetMat(SkinType skinType)
    {
        return materials[(int)skinType];
    }
}
