using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public bool IsPaused { get; private set; }

    public event Action OnGameResumed;
    public event Action OnGamePaused;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            Time.timeScale = 1;
            IsPaused = false;

            OnGameResumed?.Invoke();
        }
        else
        {
            Time.timeScale = 0;
            IsPaused = true;

            OnGamePaused?.Invoke();
        }
    }
}