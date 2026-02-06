using UnityEngine;

public class InitialisePlayerPositionStateBehaviour : StateBehaviour
{
    private readonly Transform _playerTransform;
    private readonly Transform _startPortalTransform;
    private readonly LevelCheckpointController _levelCheckpointController;

    public InitialisePlayerPositionStateBehaviour(
        Transform playerTransform,
        Transform startPortalTransform,
        LevelCheckpointController levelCheckpointController)
    {
        _playerTransform = playerTransform;
        _startPortalTransform = startPortalTransform;
        _levelCheckpointController = levelCheckpointController;
    }

    public override void Enter(float transitionDuration)
    {
        if (_levelCheckpointController.CurrentCheckpointPosition.HasValue)
        {
            _playerTransform.position = _levelCheckpointController.CurrentCheckpointPosition.Value;
            _playerTransform.rotation = _levelCheckpointController.CurrentCheckpointRotation.Value;
        }
        else
        {
            _playerTransform.position = _startPortalTransform.position;
            _playerTransform.rotation = _startPortalTransform.rotation;
        }

        Physics.SyncTransforms();
    }
}
