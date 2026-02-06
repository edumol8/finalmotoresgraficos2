using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectReferenceInitialiser : MonoBehaviour
{
    [SerializeField]
    private GameObjectReference _gameObjectReference;

    private void Start()
    {
        _gameObjectReference.Initialise(gameObject);
    }

    private void OnDestroy()
    {
        _gameObjectReference.Clear();
    }
}
