using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private HealthController _healthController;

    [SerializeField]
    private GameObject _heartPrefab;

    private void Start()
    {
        _healthController.OnHealthChanged += UpdateHealthBar;
        
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        UpdateNumberOfHearts();
        UpdateHeartColors();
    }

    private void UpdateNumberOfHearts()
    {
        int numberOfMissingHearts = _healthController.CurrentHealth - transform.childCount;

        if (numberOfMissingHearts <= 0)
        {
            return;
        }

        for (int i = 0; i < numberOfMissingHearts; i++)
        {
            Instantiate(_heartPrefab, transform);
        }
    }

    private void UpdateHeartColors()
    {
        int activeHearts = 0;

        foreach (Transform child in transform)
        {
            if (activeHearts < _healthController.CurrentHealth)
            {
                child.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                activeHearts++;
            }
            else
            {
                child.GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }
}
