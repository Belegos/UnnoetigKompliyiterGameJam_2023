using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class TimeCounter : MonoBehaviour
{
    [FormerlySerializedAs("_timerText")] public TMP_Text TimerText;

    private float _currentTime = 0.0f;

    

    private bool _isPlaying = false;

    #region Properties

    public bool IsPlaying
    {
        get
        {
            return _isPlaying;
        }
        set
        {
            _isPlaying = value;
        }
    }
    
    public float CurrentTime
    {
        get => _currentTime;
    }

    #endregion

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaying)
        {
            _currentTime += Time.deltaTime;
            TimerText.text = TimeSpan.FromSeconds(_currentTime).ToString(@"m\:ss\.fff"); //Format for minutes ,seconds and milliseconds only
        }
    }

    private void ResetTime()
    {
        _currentTime = 0.0f;
    }

    public void StartGame()
    {
        _isPlaying = true;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
