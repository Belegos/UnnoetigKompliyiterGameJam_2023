using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateTrap : MonoBehaviour
{
    private float _openTime = 20f;
    private float _closeTime = 10f;
    private float _waitTime = 1f;
    [SerializeField] private Vector3 rotationAmount = new Vector3(0f, 90f, 0f);
    [Header("DATA")]
    public SO_TrapData TrapData;
    [HideInInspector] public bool TrapSettingsFoldout = true;

    private void Awake()
    {
        if (TrapData != null)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        _openTime = TrapData.OpenTime;
        _closeTime = TrapData.CloseTime;
        _waitTime = TrapData.WaitTime;
    }

    public void OnTrapSettingsUpdated()
    {
        Initialize();
    }
    
    public IEnumerator RotateCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + rotationAmount);

        float elapsedTime = 0f;
        while (elapsedTime < _openTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / _openTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;

        yield return new WaitForSeconds(_waitTime);
        // Start the reverse rotation coroutine
        StartCoroutine(RotateBackCoroutine(startRotation, _closeTime));
    }

    private IEnumerator RotateBackCoroutine(Quaternion startRotation, float duration)
    {
        Quaternion targetRotation = startRotation;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        transform.rotation = targetRotation;
    }
}
