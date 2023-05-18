using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "WorldGeneration/Wordgen_data")]
public class SO_WorldData : ScriptableObject
{
    [SerializeField] GameObject[] prefab_tile;
    [SerializeField] GameObject[] prefab_falseTrap;
    [SerializeField] GameObject[] prefab_trueTrap;
}
