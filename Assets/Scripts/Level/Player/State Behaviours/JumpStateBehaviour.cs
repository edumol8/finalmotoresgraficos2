using UnityEngine;

public class JumpStateBehaviour : StateBehaviour
{
    private Rigidbody _rigidbody;
    private float _jumpSpeed;

    public JumpStateBehaviour(
        Rigidbody rigidbody,
        float jumpSpeed)
    {
        _rigidbody = rigidbody;
        _jumpSpeed = jumpSpeed;
    }

    public override void Enter(float transitionDuration)
    {
        float ySpeed = Mathf.Max(_rigidbody.linearVelocity.y, _jumpSpeed);
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, ySpeed, _rigidbody.linearVelocity.z);
    }
}
