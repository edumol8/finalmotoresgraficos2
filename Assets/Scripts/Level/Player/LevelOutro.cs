using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOutro : MonoBehaviour
{
    [SerializeField]
    private Transform _startTransform;

    private FootstepController _footstepController;

    private void Awake()
    {
        _footstepController = GetComponent<FootstepController>();
    }

    public void SetStartPosition()
    {
        transform.SetPositionAndRotation(_startTransform.position, _startTransform.rotation);
        Physics.SyncTransforms();
    }

    public void StopFootstepController()
    {
        _footstepController.enabled = false;
    }
}
