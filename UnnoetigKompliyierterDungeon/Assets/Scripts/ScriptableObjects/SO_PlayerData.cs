using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/PlayerData")]
public class SO_PlayerData : ScriptableObject
{
    public string Name;
    public int Health;
}
