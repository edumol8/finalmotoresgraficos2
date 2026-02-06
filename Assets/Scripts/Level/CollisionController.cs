using System;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public event Action<Collision> OnEnterCollision;
    public event Action<Collision> OnExitCollision;
    public event Action<Collision> OnStayCollision;

    public event Action<Collider> OnEnterTrigger;
    public event Action<Collider> OnExitTrigger;
    public event Action<Collider> OnStayTrigger;

    private void OnCollisionEnter(Collision collision)
    {
        OnEnterCollision?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnExitCollision?.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        OnStayCollision?.Invoke(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnterTrigger?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExitTrigger?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
        OnStayTrigger?.Invoke(other);
    }
}
