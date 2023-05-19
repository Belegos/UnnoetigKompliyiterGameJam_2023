using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class GoalLine : MonoBehaviour
{
    public UnityEvent OnPlayerEntering;
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if player enters
        OnPlayerEntering.Invoke();
    }
}
