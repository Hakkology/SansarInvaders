using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SansarGridData", menuName = "Sansar/SansarGridData")]
public class SansarGridSO : ScriptableObject
{
    [Header("Invader grid i�lemleri")]
    public Sansar[] sansarPrefab;
    public int rows = 5;
    public int columns = 11;
    public float satirAraligi = 2.0f;
    public float sutunAraligi = 1.5f;
    //public float yOffset = 3f;

    [Header("Invader zorluk i�lemleri")]
    public float minSpeed = .5f;
    public float maxSpeed = 2f;
}
