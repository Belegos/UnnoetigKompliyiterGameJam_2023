using System;
using TMPro;
using UnityEngine;
using Cursor = UnityEngine.Cursor;
using Slider = UnityEngine.UI.Slider;

public class TimeCounter : MonoBehaviour
{
    public TMP_Text TimerText;
    [SerializeField] private Slider _healthBar;

    private float _currentTime = 0.0f;

    private PlayerData _player;

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
        _player = FindObjectOfType<PlayerData>();
        _player.OnHealthReduction.AddListener(UpdateHealthBar);
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

    private void UpdateHealthBar()
    {
        _healthBar.value = _player.Health;
    }
}
