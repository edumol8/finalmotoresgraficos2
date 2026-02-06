using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Collider _collider;

    private IEnumerable<ICollectableBehaviour> _collectableBehaviours;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collectableBehaviours = GetComponents<ICollectableBehaviour>();
    }

    public void Collect(GameObject player)
    {
        _collider.enabled = false;

        foreach (var collectableBehaviour in _collectableBehaviours)
        {
            collectableBehaviour.OnCollected(player);
        }
    }
}
