public class HasMovementInputStateTransition : StateTransition
{
    private PlayerInputController _playerInputController;
    private float _requiredSqrMovmentInputMagnitude;

    public HasMovementInputStateTransition(State targetState, float duration, PlayerInputController playerInputController, float requiredMovementInputMagnitude)
        : base(targetState, duration)
    {
        _playerInputController = playerInputController;
        _requiredSqrMovmentInputMagnitude = requiredMovementInputMagnitude * requiredMovementInputMagnitude;
    }

    public override bool IsSatisfied()
    {
        return _playerInputController.SmoothedMovementInputVector.sqrMagnitude > _requiredSqrMovmentInputMagnitude;
    }
}

