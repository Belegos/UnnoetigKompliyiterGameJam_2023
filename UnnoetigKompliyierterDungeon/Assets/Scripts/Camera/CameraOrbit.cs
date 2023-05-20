using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOrbit : MonoBehaviour
{
    #region Fields

    //GameObject _player;
    [SerializeField] Transform target;
    //[SerializeField] private Rigidbody _rb;

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float verticalRotation = 0f;

    private Vector3 offset;

    #endregion

    private void Start()
    {
        // _player = GameObject.Find("Player");
        // _rb = _player.GetComponent<Rigidbody>();
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        //if (_rb.velocity.z > 0)
        //{
        //    verticalRotation = rotationSpeed * Time.deltaTime;
        //    verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f);
        //}

        
        
        verticalRotation += InputSystem.GetDevice<Keyboard>().wKey.ReadValue() * rotationSpeed; // Vertikale Rotation (Mausbewegung oben/unten)
        verticalRotation = Mathf.Clamp(verticalRotation, -60f, 60f); // Optional: Begrenze die vertikale Rotation auf einen bestimmten Bereich

        Quaternion rotation = Quaternion.Euler(verticalRotation, 0f, 0f); // Berechne die Gesamtrotation

        transform.position = target.position + offset; // Positioniere die Kamera relativ zum Spieler
        transform.rotation = rotation; // Setze die Rotation des Kamera-Orbits
        Debug.Log(rotation);
    }
}
