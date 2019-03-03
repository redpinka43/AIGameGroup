using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat : MonoBehaviour
{
    private int baseValue;
    [SerializeField]
    public int GetValue ()
    {
        return baseValue;
    }
}
