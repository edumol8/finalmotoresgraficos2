using UnityEngine;

public class LandingCompleteStateTransition : StateTransition
{
    private PlayerInputController _playerInputController;
    private float _minimumMovementInputSqrMagnitude;
    private float _maximumMovementInputSqrMagnitude;
    private float _elapsedTimeRequired;
    private float _startTime;

    public LandingCompleteStateTransition(
        State targetState,
        float duration,
        PlayerInputController playerInputController,
        float elapsedTimeRequired,
        float minimumMovementInputMagnitude,
        float maximumMovementInputMagnitude)
        : base(targetState, duration)
    {
        _playerInputController = playerInputController;
        _elapsedTimeRequired = elapsedTimeRequired;
        _minimumMovementInputSqrMagnitude = minimumMovementInputMagnitude * minimumMovementInputMagnitude;
        _maximumMovementInputSqrMagnitude = maximumMovementInputMagnitude * maximumMovementInputMagnitude;
    }

    public override void SetUp()
    {
        _startTime = Time.time;
    }

    public override bool IsSatisfied()
    {
        if (_playerInputController.MovementInputVector.sqrMagnitude < _minimumMovementInputSqrMagnitude)
        {
            return false;
        }

        if (_playerInputController.MovementInputVector.sqrMagnitude > _maximumMovementInputSqrMagnitude)
        {
            return false;
        }

        return Time.time - _startTime >= _elapsedTimeRequired;
    }
}
