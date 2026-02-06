using UnityEngine;

public class IsGroundedStateTransition : StateTransition
{
    private readonly GroundController _groundController;
    private readonly Rigidbody _rigidbody;

    public IsGroundedStateTransition(
        State targetState,
        float duration,
        GroundController groundController,
        Rigidbody rigidbody)
        : base(targetState, duration)
    {
        _groundController = groundController;
        _rigidbody = rigidbody;
    }

    public override bool IsSatisfied()
    {
        return _groundController.IsGrounded && _rigidbody.linearVelocity.y <= 1;
    }
}