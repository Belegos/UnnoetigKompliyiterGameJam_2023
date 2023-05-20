using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public UnityEvent OnHealthReduction;
    public UnityEvent OnDeath;
    public SO_PlayerData Data;
    [HideInInspector] public bool PlayerDataFoldout = true;

    private string _name = "";

    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (_health == value) return;
            _health = value;
            OnHealthReduction.Invoke();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (Data != null)
        {
            PlayerSettingsUpdated();
        }

        OnHealthReduction.AddListener(CheckHealth);
    }

    public void PlayerSettingsUpdated()
    {
        _name = Data.Name;
        Health = Data.Health;
    }

    public void ReduceHealth(int damage)
    {
        Health = _health - damage;
    }

    private void CheckHealth()
    {
        if (_health <= 0)
        {
            OnDeath.Invoke();
        }
    }
}
