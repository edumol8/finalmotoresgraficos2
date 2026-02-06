using UnityEngine;

public class IsFallingStateTransition : StateTransition
{
    private GroundController _groundController;
    private float _coyoteTime;
    private readonly float _minimumDistanceAboveGround;

    public IsFallingStateTransition(
        State targetState,
        float duration,
        GroundController groundController,
        float coyoteTime,
        float minimumDistanceAboveGround)
        : base(targetState, duration)
    {
        _groundController = groundController;
        _coyoteTime = coyoteTime;
        _minimumDistanceAboveGround = minimumDistanceAboveGround;
    }

    public override bool IsSatisfied()
    {
        if (_groundController.GroundedRecently(_coyoteTime))
        {
            return false;
        }

        return _groundController.DistanceToGround.HasValue == false || _groundController.DistanceToGround >= _minimumDistanceAboveGround;
    }
}