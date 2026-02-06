using UnityEngine;

public class CoinsCollectedUI : MonoBehaviour, IPostRestoreSaveDataComponent
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    [SerializeField]
    private TMPro.TMP_Text _coinsCollectedText;

    private LivesController _playerLivesController;

    public void PostRestoreSaveData()
    {
        UpdateCoinsCollectedText();
    }

    private void Awake()
    {
        _playerLivesController = _playerInventory.GetComponent<LivesController>();
    }

    private void Start()
    {
        _playerInventory.OnNumberOfCoinsChanged += UpdateCoinsCollectedText;
    }

    private void UpdateCoinsCollectedText()
    {
        if (_playerLivesController.NumberOfLives > 0)
        {
            _coinsCollectedText.text = $"{_playerInventory.NumberOfCoins:D3}";
        }
    }
}
