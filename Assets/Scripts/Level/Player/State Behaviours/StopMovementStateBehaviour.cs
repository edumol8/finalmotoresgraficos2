using UnityEngine;

public class StopMovementStateBehaviour : StateBehaviour
{
    private readonly Rigidbody _rigidbody;
    private readonly Collider _collider;
    private PhysicsMaterial _stationaryPhysicMaterial;
    private PhysicsMaterial _movingPhysicMaterial;

    public StopMovementStateBehaviour(Rigidbody rigidbody, Collider collider, PhysicsMaterial stationaryPhysicMaterial, PhysicsMaterial movingPhysicMaterial)
    {
        _rigidbody = rigidbody;
        _collider = collider;
        _stationaryPhysicMaterial = stationaryPhysicMaterial;
        _movingPhysicMaterial = movingPhysicMaterial;
    }

    public override void Enter(float transitionDuration)
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _collider.material = _stationaryPhysicMaterial;
    }

    public override void Exit()
    {
        _collider.material = _movingPhysicMaterial;
    }
}
