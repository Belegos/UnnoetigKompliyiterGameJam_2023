using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SO_TrapData TrapData;
    [HideInInspector] public bool TrapSettingsFoldout = true;
    [SerializeField] private RotateTrap _rotateTrap; //Collider that is a trigger on the GO
    [SerializeField] private MoveTrap _moveTrap; //Collider that is a trigger on the GO
    private string _name;
    private int _damage;
    private Coroutine _coroutine = null;


    public int Damage{get { return _damage;} }
    
    // Start is called before the first frame update
    void Start()
    {
        if (TrapData != null)
        {
            Initialize();
        }
    }

    /// <summary>
    /// Set all data from the SO in here, so that it will be updated on Start and when stuff gets changed in editor.
    /// </summary>
    private void Initialize()
    {
        _name = TrapData.Name;
        _damage = TrapData.Damage;
    }

    public void OnTrapSettingsUpdated()
    {
        Initialize();
    }

    /// <summary>
    /// Start the trap animation in here.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _coroutine != null) return;
        _coroutine = StartCoroutine(StartTrapCounter());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") || _coroutine != null) return;
        _coroutine = StartCoroutine(StartTrapCounter());
        Debug.Log("Entered Trap: " + gameObject.name);
    }

    private IEnumerator StartTrapCounter()
    {
        yield return new WaitForSeconds(0.5f);
        if (_rotateTrap != null)
        {
            yield return StartCoroutine(_rotateTrap.RotateCoroutine());
        }

        if (_moveTrap != null)
        {
            yield return StartCoroutine(_moveTrap.MoveToSpot());
        }

        yield return new WaitForSeconds(1.0f);
        _coroutine = null;
    } 
}
