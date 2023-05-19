using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BloodVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect _bloodBurst;
    private Vector3 _randomVelocity;

    public void PlayBloodVFX()
    {
        _randomVelocity = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), Random.Range(-12f, 12f));

        _bloodBurst.SetVector3("BloodVelocity", _randomVelocity);
        
        _bloodBurst.Play();
    }
}
