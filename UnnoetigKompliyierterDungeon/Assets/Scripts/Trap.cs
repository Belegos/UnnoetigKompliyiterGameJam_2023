using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Trap : MonoBehaviour
{
    public SO_TrapData TrapData;
    [HideInInspector] public bool TrapSettingsFoldout = true;
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

    private void Initialize()
    {
        _name = TrapData.Name;
        _damage = TrapData.Damage;
    }

    public void OnTrapSettingsUpdated()
    {
        Initialize();
    }
}
