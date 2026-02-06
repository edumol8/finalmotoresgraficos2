using UnityEngine;

public class AddCoinToInventoryCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    public void OnCollected(GameObject player)
    {
        player.GetComponent<PlayerInventory>().AddCoin();
    }
}
