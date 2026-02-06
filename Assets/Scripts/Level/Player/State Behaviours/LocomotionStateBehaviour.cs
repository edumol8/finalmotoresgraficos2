using UnityEngine;

public class LocomotionStateBehaviour : StateBehaviour
{
    private Animator _animator;
    private PlayerInputController _playerInputController;
    private Rigidbody _rigidbody;
    private Transform _cameraTransform;

    public LocomotionStateBehaviour(
        Animator animator,
        PlayerInputController playerInputController,
        Rigidbody rigidbody,
        Transform camera)
    {
        _animator = animator;
        _playerInputController = playerInputController;
        _rigidbody = rigidbody;
        _cameraTransform = camera;
    }

    public override void FixedUpdate()
    {
        Vector3 inputVector = _playerInputController.GetSmoothedMovementInputVectorInCameraDirection(_cameraTransform);

        _animator.SetFloat("LocomotionMagnitude", inputVector.magnitude, 0.05f, Time.deltaTime);

        Vector3 inputDirection = inputVector.normalized;
        
        Vector3 animationVelocity = new Vector3(_animator.velocity.x, 0, _animator.velocity.z);

        float sizeOfVelocityInInputDirection = Vector3.Dot(animationVelocity, inputDirection);

        Vector3 velocity;

        if (sizeOfVelocityInInputDirection > 0)
        {
            velocity = animationVelocity.normalized * sizeOfVelocityInInputDirection;
        }
        else
        {
            velocity = Vector3.zero;
        }

        velocity.y = _rigidbody.linearVelocity.y;

        _rigidbody.linearVelocity = velocity;
    }
}
