using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ActivatedTrap : MonoBehaviour
{
    [SerializeField]private Trap _trap;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Damage Trap: " + gameObject.name);
        other.GetComponent<PlayerData>().ReduceHealth(_trap.Damage);
    }
}
