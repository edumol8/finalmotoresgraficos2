using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReturnToMainMenu : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController;

    private void OnSubmit(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _sceneController.LoadScene(SceneIndex.MainMenu);
        }
    }
}
