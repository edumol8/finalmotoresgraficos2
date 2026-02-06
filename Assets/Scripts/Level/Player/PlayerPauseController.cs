using UnityEngine;
using UnityEngine.InputSystem;

internal class PlayerPauseController : MonoBehaviour
{
    [SerializeField]
    private PauseController _pauseController;

    private void OnPause(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _pauseController.TogglePause();
        }
    }
}
