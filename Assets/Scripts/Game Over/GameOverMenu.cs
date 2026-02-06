using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController;

    [SerializeField]
    private SaveController _saveController;

    public void Yes()
    {
        int savedSceneIndex = _saveController.GetSavedSceneIndex();

        _sceneController.LoadScene((SceneIndex)savedSceneIndex);
    }

    public void No()
    {
        _sceneController.LoadScene(SceneIndex.MainMenu);
    }
}
