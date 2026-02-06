public class NoMovementInputStateTransition : StateTransition
{
    private PlayerInputController _playerInputController;
    private float _noMovementInputSqrMagnitude;

    public NoMovementInputStateTransition(State targetState, float duration, PlayerInputController playerInputController, float noMovementInputMagnitude)
        : base(targetState, duration)
    {
        _playerInputController = playerInputController;
        _noMovementInputSqrMagnitude = noMovementInputMagnitude * noMovementInputMagnitude;
    }
    
    
    public override bool IsSatisfied()
    {
        return _playerInputController.MovementInputVector.sqrMagnitude < _noMovementInputSqrMagnitude
            && _playerInputController.SmoothedMovementInputVector.sqrMagnitude < _noMovementInputSqrMagnitude;
    }
}

