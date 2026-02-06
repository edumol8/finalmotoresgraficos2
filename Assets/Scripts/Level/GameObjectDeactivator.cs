using UnityEngine;

public class GameObjectDeactivator : MonoBehaviour
{
    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}