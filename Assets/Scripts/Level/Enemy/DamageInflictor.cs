using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInflictor : MonoBehaviour
{
    [SerializeField]
    private int _damageAmount;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == false)
        {
            return;
        }

        var invincibilityController = collision.gameObject.GetComponent<InvincibilityController>();

        if (invincibilityController.IsInvincible)
        {
            return;
        }

        var healthController = collision.gameObject.GetComponent<HealthController>();
        healthController.RemoveHealth(_damageAmount);
    }
}
