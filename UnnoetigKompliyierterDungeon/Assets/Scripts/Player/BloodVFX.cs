using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BloodVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect _bloodBurst;
    private float _randomAngle;
    private Quaternion _randomRotation;

    public void PlayBloodVFX()
    {
        // Generate a random angle around the y-axis
        _randomAngle = Random.Range(0f, 360f);

        // Create a Quaternion representing the rotation around the y-axis
        _randomRotation = Quaternion.Euler(0f, _randomAngle, 0f);

        // Apply the random rotation to the GameObject's transform
        transform.rotation = _randomRotation;
        
        _bloodBurst.Play();
    }
}
