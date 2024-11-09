using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SansarData", menuName = "Sansar/SansarData")]
public class SansarSO : ScriptableObject
{
    public Sprite[] animasyonSprites;
    public float animasyonSuresi = 1.0f;
}
