using System;
using UnityEngine;

public class WaterDetector : MonoBehaviour
{
    public event Action OnSplashEntry;

    public event Action OnUnderwater;

    [field: SerializeField]
    public float UnderwaterYPosition { get; private set; }

    [SerializeField]
    private float _surfaceYPosition;
        
    [SerializeField]
    private float _requiredSplashSpeed;

    private bool _onSplashEntryEventTriggered;
    private bool _onUnderwaterEventTriggered;
    private Rigidbody _rigidBody;
    private GroundController _groundController;
    private float _requiredSplashSqrSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _groundController = GetComponent<GroundController>();
        _requiredSplashSqrSpeed = _requiredSplashSpeed * _requiredSplashSpeed;
    }

    private void Update()
    {
        if (_onSplashEntryEventTriggered == false)
        {
            if (transform.position.y <= _surfaceYPosition && _rigidBody.linearVelocity.sqrMagnitude >= _requiredSplashSqrSpeed)
            {
                if (_groundController == null || _groundController.GroundPosition.HasValue == false)
                {
                    OnSplashEntry?.Invoke();
                    _onSplashEntryEventTriggered = true;
                }
            }
        }

        if (_onUnderwaterEventTriggered == false)
        {
            if (transform.position.y <= UnderwaterYPosition)
            {
                OnUnderwater?.Invoke();
                _onUnderwaterEventTriggered = true;
            }
        }
    }
}
