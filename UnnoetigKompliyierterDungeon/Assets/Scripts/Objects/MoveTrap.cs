using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveTrap : MonoBehaviour
{
    [SerializeField] private GameObject _destination;
    [Header("DATA")]
    public SO_TrapData TrapData;
    [HideInInspector] public bool TrapSettingsFoldout = true;

    
    private float _openTime = 2f;
    private float _closeTime = 2f;
    private float _waitTime = 2f;

    private Vector3 startPos;
    private float elapsedTime;

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

    public IEnumerator MoveToSpot()
    {
        startPos = transform.position;

        while (elapsedTime < _openTime)
        {
            transform.position = Vector3.Lerp(startPos, _destination.transform.position, (elapsedTime / _openTime));
            elapsedTime += Time.deltaTime;
            yield return null;

        }
        transform.position = _destination.transform.position;
        yield return new WaitForSeconds(_waitTime);
        StartCoroutine(MoveBackCoroutine(startPos, _closeTime));

    }
    
    private IEnumerator MoveBackCoroutine(Vector3 startPosition, float duration)
    {
        Vector3 targetPosition = startPosition;

        float timer = 0f;
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, (timer / duration));
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}
