using UnityEngine;

public class AirMovementStateBehaviour : StateBehaviour
{
    private PlayerInputController _playerInputController;
    private Transform _cameraTransform;
    private readonly Rigidbody _rigidbody;
    private float _maximumHorizontalJumpSpeed;

    public AirMovementStateBehaviour(
        PlayerInputController playerInputController,
        Transform cameraTransform,
        float maximumHorizontalJumpSpeed,
        Rigidbody rigidbody)
    {
        _playerInputController = playerInputController;
        _cameraTransform = cameraTransform;
        _maximumHorizontalJumpSpeed = maximumHorizontalJumpSpeed;
        _rigidbody = rigidbody;
    }

    public override void FixedUpdate()
    {
        Vector3 requiredVelocity = _playerInputController.GetMovementInputVectorInCameraDirection(_cameraTransform) * _maximumHorizontalJumpSpeed;
        requiredVelocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = requiredVelocity;
    }
}
