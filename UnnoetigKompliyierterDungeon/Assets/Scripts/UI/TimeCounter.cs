using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private float _currentTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        _timerText.text = TimeSpan.FromSeconds(_currentTime).ToString(@"m\:ss\.fff"); //Format for minutes ,seconds and milliseconds only
    }

    private void ResetTime()
    {
        _currentTime = 0.0f;
    }
}
