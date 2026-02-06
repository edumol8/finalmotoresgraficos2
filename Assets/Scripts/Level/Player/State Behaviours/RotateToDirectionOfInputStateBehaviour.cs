using UnityEngine;

public class RotateToDirectionOfInputStateBehaviour : StateBehaviour
{
    private PlayerInputController _playerInputController;
    private Rigidbody _rigidbody;
    private Transform _cameraTransform;
    private float _rotationSpeed;

    public RotateToDirectionOfInputStateBehaviour(
        PlayerInputController playerInputController,
        Rigidbody rigidbody,
        Transform cameraTransform,
        float rotationSpeed)
    {
        _playerInputController = playerInputController;
        _rigidbody = rigidbody;
        _cameraTransform = cameraTransform;
        _rotationSpeed = rotationSpeed;
    }

    public override void FixedUpdate()
    {
        if (_playerInputController.MovementInputVector != Vector2.zero)
        {
            Vector3 targetDirection = _playerInputController.GetMovementInputVectorInCameraDirection(_cameraTransform);

            Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }        
    }
}
