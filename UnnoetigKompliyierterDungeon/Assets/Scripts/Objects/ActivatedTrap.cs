using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ActivatedTrap : MonoBehaviour
{
    [SerializeField]private Trap _trap;
    [SerializeField]private BloodVFX _bloodVFX;

    private PlayerData _playerData;
    private void Start()
    {
        _playerData = FindObjectOfType<PlayerData>();
        if (_playerData == null) return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Vector3 trans = other.transform.position;
        _bloodVFX.transform.position = new Vector3(trans.x, 1.0f, trans.z);
        _bloodVFX.PlayBloodVFX();
        other.GetComponent<PlayerData>().ReduceHealth(_trap.Damage);
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (!other.gameObject.CompareTag("Player")) return;
    //     Vector3 trans = other.transform.position;
    //     _bloodVFX.transform.position = new Vector3(trans.x, 1.0f, trans.z);
    //     _bloodVFX.PlayBloodVFX();
    //     other.gameObject.GetComponent<PlayerData>().ReduceHealth(_trap.Damage);
    // }
}
