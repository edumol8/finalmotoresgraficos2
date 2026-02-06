using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private float _movementInputAcceleration;

    [SerializeField]
    private PauseController _pauseController;

    public Vector2 MovementInputVector { get; private set; }

    public Vector2 SmoothedMovementInputVector { get; private set; }

    public event Action OnJumpButtonPressed;

    public Vector3 GetSmoothedMovementInputVectorInCameraDirection(Transform cameraTransform)
    {
        return GetInputVectorInCameraDirection(new Vector3(SmoothedMovementInputVector.x, 0, SmoothedMovementInputVector.y), cameraTransform);
    }

    public Vector3 GetMovementInputVectorInCameraDirection(Transform cameraTransform)
    {
        return GetInputVectorInCameraDirection(new Vector3(MovementInputVector.x, 0, MovementInputVector.y), cameraTransform);
    }

    private Vector3 GetInputVectorInCameraDirection(Vector3 inputVector, Transform cameraTransform)
    {
        return Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * inputVector;
    }

    private void Update()
    {
        SmoothedMovementInputVector = Vector2.MoveTowards(SmoothedMovementInputVector, MovementInputVector, _movementInputAcceleration * Time.deltaTime);
    }

    private void OnMove(InputValue inputValue)
    {
        MovementInputVector = inputValue.Get<Vector2>();

        if (MovementInputVector.sqrMagnitude > 1)
        {
            MovementInputVector = MovementInputVector.normalized;
        }
    }

    private void OnJump(InputValue inputValue)
    {
        if (_pauseController.IsPaused)
        {
            return;
        }

        if (inputValue.isPressed)
        {
            OnJumpButtonPressed?.Invoke();
        }
    }
}
