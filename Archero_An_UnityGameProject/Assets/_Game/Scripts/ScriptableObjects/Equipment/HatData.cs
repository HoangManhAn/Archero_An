using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "HatData")]
public class HatData : ScriptableObject
{
    [SerializeField] Hat[] hats;

    public Hat GetHat(HatType hatType)
    {
        return hats[(int)hatType];
    }
}
