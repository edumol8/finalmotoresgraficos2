using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Object Reference", menuName = "Scriptable Objects/Game Object Reference", order = 1)]
public class GameObjectReference : ScriptableObject
{
    public GameObject GameObject { get; private set; }

    public void Initialise(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    internal void Clear()
    {
        GameObject = null;
    }
}
