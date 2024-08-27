using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance => _instance;

    private bool _isRestarting = false;

    [SerializeField] private float _restartLevelAfterGameOverDelay = 1.5f;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void RestartLevelAfterGameOver() => RestartLevel(_restartLevelAfterGameOverDelay);

    public void RestartLevel(float delay = 0f)
    {
        if (_isRestarting) return;
        _isRestarting = true;
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ExecuteLoadLevel(currSceneIndex, delay));
    }

    private IEnumerator ExecuteLoadLevel(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}