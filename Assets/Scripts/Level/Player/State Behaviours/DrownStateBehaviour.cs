using UnityEngine;

public class DrownStateBehaviour : StateBehaviour
{
    private readonly GravityController _gravityController;
    private readonly Rigidbody _rigidbody;
    private readonly WaterDetector _underwaterDetector;

    public DrownStateBehaviour(
        GravityController gravityController,
        Rigidbody rigidbody,
        WaterDetector underwaterDetector)
    {
        _gravityController = gravityController;
        _rigidbody = rigidbody;
        _underwaterDetector = underwaterDetector;
    }

    public override void Enter(float transitionDuration)
    {
        _gravityController.GravityMultiplier = 0;
        _rigidbody.position = new Vector3(_rigidbody.position.x, _underwaterDetector.UnderwaterYPosition, _rigidbody.position.z);
    }
}
