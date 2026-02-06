using System;
using UnityEngine;

public class EndPortalDetector : MonoBehaviour
{
    public event Action OnEndPortalReached;

    [SerializeField]
    private Transform _endPortalTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _endPortalTransform)
        {
            OnEndPortalReached?.Invoke();
        }
    }
}
