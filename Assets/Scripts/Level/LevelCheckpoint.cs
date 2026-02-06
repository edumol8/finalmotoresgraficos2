using UnityEngine;

public class LevelCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LevelCheckpointController checkpointController))
        {
            checkpointController.SetCurrentCheckpoint(this);
        }
    }
}
