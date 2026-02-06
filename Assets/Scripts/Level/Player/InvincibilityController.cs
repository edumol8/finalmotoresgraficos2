using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvincibilityController : MonoBehaviour
{
    public bool IsInvincible { get; private set; }

    public float InvincibilityDuration { get; private set; }

    public event Action InvincibilityStarted;
    
    public void StartInvincibility(float invincibilityDuration)
    {
        StopAllCoroutines();
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        IsInvincible = true;
        InvincibilityDuration = invincibilityDuration;
        InvincibilityStarted?.Invoke();

        while (InvincibilityDuration > 0)
        {
            yield return null;
            InvincibilityDuration -= Time.deltaTime;
        }

        InvincibilityDuration = 0;
        IsInvincible = false;
    }
}

