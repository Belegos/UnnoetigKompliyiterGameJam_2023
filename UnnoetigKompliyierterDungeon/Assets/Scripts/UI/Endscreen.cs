using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class Endscreen : MonoBehaviour
{
    [Header("GameObjects")] 
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject _playScreen;
    
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _timeValue;
    [SerializeField] private TimeCounter _timeCounter;

    private string _filePath = "times.txt";
    private bool _isActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputField.characterLimit = 8;
    }

    public void SavePlayerData()
    {
        PlayerDataSaveClass data = new PlayerDataSaveClass();

        if (!File.Exists(_filePath))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(_filePath))
            {
                if (_inputField.text == string.Empty)
                    data.Name = "ToPretentiousToEnterAName";
                else
                    data.Name = _inputField.text;
                
                data.Time = TimeSpan.FromSeconds(_timeCounter.CurrentTime).ToString(@"m\:ss\.fff");
                sw.WriteLine($"Name: {data.Name} <-------------------> Time: {data.Time}");
            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(_filePath))
            {
                if (_inputField.text == string.Empty)
                    data.Name = "ToPretentiousToEnterAName";
                else
                    data.Name = _inputField.text;
                
                data.Time = TimeSpan.FromSeconds(_timeCounter.CurrentTime).ToString(@"m\:ss\.fff");
                sw.WriteLine($"Name: {data.Name} <-------------------> Time: {data.Time}");
            }
        }
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneManager.LoadSceneAsync("MenuScene");
    }

    public void OpenEndScreen()
    {
        if (_isActivated) return;
        _isActivated = true;
        if (_timeCounter == null) return;
        _timeValue.text = TimeSpan.FromSeconds(_timeCounter.CurrentTime).ToString(@"m\:ss\.fff");
        _endScreen.SetActive(true);
        _playScreen.SetActive(false);
        Time.timeScale = 0f;
    }
}

[System.Serializable]
public class PlayerDataSaveClass
{
    public string Name;
    public string Time;
}