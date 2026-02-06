using UnityEngine;

public class InvincibilityMaterialFlash : MonoBehaviour
{
    [SerializeField]
    private Color _flashColor;

    [SerializeField]
    private float _flashesPerSecond;

    private MaterialFlash _materialFlash;
    private InvincibilityController _invincibilityController;

    private void Awake()
    {
        _materialFlash = GetComponent<MaterialFlash>();
        _invincibilityController = GetComponent<InvincibilityController>();
        _invincibilityController.InvincibilityStarted += InvincibilityStarted;
    }

    private void InvincibilityStarted()
    {
        _materialFlash.StartFlash(_invincibilityController.InvincibilityDuration, _flashColor, _flashesPerSecond);
    }
}


