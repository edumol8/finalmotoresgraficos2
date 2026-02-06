using System;
using UnityEngine;

internal class PauseScreenUIController : MonoBehaviour
{
    [SerializeField]
    private PauseController _pauseController;

    [SerializeField]
    private Canvas _pauseScreenUI;

    private void Start()
    {
        _pauseController.OnGamePaused += ActivatePauseScreen;
        _pauseController.OnGameResumed += DeactivatePauseScreen;
    }

    private void ActivatePauseScreen()
    {
        _pauseScreenUI.gameObject.SetActive(true);
    }

    private void DeactivatePauseScreen()
    {
        _pauseScreenUI.gameObject.SetActive(false);
    }
}

