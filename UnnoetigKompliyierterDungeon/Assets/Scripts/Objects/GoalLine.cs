using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GoalLine : MonoBehaviour
{
    public UnityEvent OnPlayerEntering;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        OnPlayerEntering.Invoke();
    }
}
