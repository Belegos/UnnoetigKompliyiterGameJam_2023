using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTrapData", menuName = "Trap/TrapData")]
public class SO_TrapData : ScriptableObject
{
    public string Name;
    public int Damage;
}
