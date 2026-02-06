using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SceneController _sceneController;

    [SerializeField]
    private SaveController _saveController;

    [SerializeField]
    private Button _resumeButton;

    [SerializeField]
    private Button _newGameButton;

    [SerializeField]
    private RectTransform _menuPanel;

    [SerializeField]
    private int _reducedMenuPanelHeight;

    [SerializeField]
    private EventSystem _eventSystem;

    private void Start()
    {
        if (_saveController.SaveDataExists() == false)
        {
            _resumeButton.gameObject.SetActive(false);
            _menuPanel.sizeDelta = new Vector2(_menuPanel.sizeDelta.x, _reducedMenuPanelHeight);
            _eventSystem.SetSelectedGameObject(_newGameButton.gameObject);
        }
    }

    public void Resume()
    {
        int savedSceneIndex = _saveController.GetSavedSceneIndex();

        _sceneController.LoadScene((SceneIndex)savedSceneIndex);
    }

    public void NewGame()
    {
        _saveController.ClearSaveData();
        _sceneController.LoadScene(SceneIndex.Level1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
