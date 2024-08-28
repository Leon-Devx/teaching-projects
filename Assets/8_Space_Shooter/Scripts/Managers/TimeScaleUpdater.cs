using UnityEngine;

public class TimeScaleUpdater : MonoBehaviour
{
    private void OnEnable()
    {
        SettingsUI.OnAnyClickPauseButton += PauseGame;
        SettingsUI.OnAnyClickResumeButton += ResumeGame;
        SettingsUI.OnAnyClickRestartButton += ResumeGame;
    }

    private void OnDisable()
    {
        SettingsUI.OnAnyClickPauseButton -= PauseGame;
        SettingsUI.OnAnyClickResumeButton -= ResumeGame;
        SettingsUI.OnAnyClickRestartButton -= ResumeGame;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}