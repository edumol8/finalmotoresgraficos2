using UnityEngine;

public class PlayerInvincibilityController : MonoBehaviour
{
    [SerializeField]
    private float _invincibilityDuration;

    private HealthController _healthController;
    private InvincibilityController _invincibilityController;

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
        _healthController = GetComponent<HealthController>();
        _healthController.OnDamaged += OnDamaged;
    }

    private void OnDamaged()
    {
        _invincibilityController.StartInvincibility(_invincibilityDuration);
    }
}

