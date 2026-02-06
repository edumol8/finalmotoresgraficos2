using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int _currentHealth;

    [SerializeField]
    private int _maximumHealth;

    public event Action OnDied;

    public event Action OnDamaged;

    public event Action OnHealthChanged;

    [field: SerializeField]
    public int InitialHealth { get; private set; }

    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke();
        }
    }

    public void RemoveHealth(int amount)
    {
        if (CurrentHealth == 0)
        {
            return;
        }

        CurrentHealth -= amount;

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        if (CurrentHealth == 0)
        {
            OnDied?.Invoke();
        }
        else
        {
            OnDamaged?.Invoke();
        }
    }

    public void AddHealth(int amount)
    {
        if (CurrentHealth == _maximumHealth)
        {
            return;
        }

        CurrentHealth += amount;

        if (CurrentHealth > _maximumHealth)
        {
            CurrentHealth = _maximumHealth;
        }
    }

    private void Start()
    {
        CurrentHealth = InitialHealth;
    }
}
