using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsUI : MonoBehaviour
{
    public static event Action OnAnyClickPauseButton;
    public static event Action OnAnyClickResumeButton;
    public static event Action OnAnyClickRestartButton;
    
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        _settingsPanel.SetActive(false);
        
        _pauseButton.onClick.AddListener(OnClickPauseButton);
        _resumeButton.onClick.AddListener(OnClickResumeButton);
        _restartButton.onClick.AddListener(OnClickRestartButton);
    }

    private void OnClickPauseButton()
    {
        _settingsPanel.SetActive(true);
        OnAnyClickPauseButton?.Invoke();
    }

    private void OnClickResumeButton()
    {
        _settingsPanel.SetActive(false);
        OnAnyClickResumeButton?.Invoke();
    }

    private void OnClickRestartButton()
    {
        _settingsPanel.SetActive(false);
        OnAnyClickRestartButton?.Invoke();
        _pauseButton.interactable = false;
    }
}
