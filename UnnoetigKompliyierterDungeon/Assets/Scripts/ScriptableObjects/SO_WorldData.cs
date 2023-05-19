using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "WorldGeneration/Wordgen_data")]
public class SO_WorldData : ScriptableObject
{
    [SerializeField] public GameObject[] prefab_tile;
    [SerializeField] public GameObject[] prefab_falseTrap;
    [SerializeField] public GameObject[] prefab_trueTrap;
    [SerializeField] public GameObject[] Wall;
    [SerializeField] public GameObject Start;
    [SerializeField] public GameObject Finish;
    [SerializeField] public GameObject BigTrap;
}
