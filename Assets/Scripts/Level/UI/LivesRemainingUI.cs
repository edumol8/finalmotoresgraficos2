using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesRemainingUI : MonoBehaviour, IPostRestoreSaveDataComponent
{
    [SerializeField]
    private LivesController _livesController;

    [SerializeField]
    private TMPro.TMP_Text _livesRemainingText;

    public void PostRestoreSaveData()
    {
        UpdateLivesRemainingText();
    }

    private void Start()
    {
        UpdateLivesRemainingText();
    }

    private void UpdateLivesRemainingText()
    {
        _livesRemainingText.text = $"x {_livesController.NumberOfLives}";
    }
}
