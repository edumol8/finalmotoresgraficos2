using System;
using UnityEngine;

internal class PauseScreenUI : MonoBehaviour
{
    [SerializeField]
    private PauseController _pauseController;

    [SerializeField]
    private SaveController _saveController;

    [SerializeField]
    private SceneController _sceneController;

    public void Resume()
    {
        _pauseController.TogglePause();
    }

    public void Exit()
    {
        _saveController.SaveGame();
        _sceneController.LoadScene(SceneIndex.MainMenu);
    }
}

