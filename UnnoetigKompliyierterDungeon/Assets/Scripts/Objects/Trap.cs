using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SO_TrapData TrapData;
    [HideInInspector] public bool TrapSettingsFoldout = true;
    [SerializeField] private BoxCollider _trapTrigger; //Collider that is a trigger on the GO
    private string _name;
    private int _damage;
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
        //TODO: Player Check and start trap via Coroutine
        StartCoroutine(StartTrapCounter());
        Debug.Log("Entered Trap: " + gameObject.name);
    }

    private IEnumerator StartTrapCounter()
    {
        yield return new WaitForSeconds(1.0f);
        // Start Trap
        Debug.Log("Started Trap: " + gameObject.name);
    } 
}
