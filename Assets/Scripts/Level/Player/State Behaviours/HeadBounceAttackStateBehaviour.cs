public class HeadBounceAttackStateBehaviour : StateBehaviour
{
    private HeadBounceCollisionController _headBounceCollisionController;

    public HeadBounceAttackStateBehaviour(HeadBounceCollisionController headBounceCollisionController)
    {
        _headBounceCollisionController = headBounceCollisionController;
    }

    public override void Enter(float transitionDuration)
    {
        _headBounceCollisionController.OnHeadBounceCollision += OnHeadBounceCollision;
    }

    public override void Exit()
    {
        _headBounceCollisionController.OnHeadBounceCollision -= OnHeadBounceCollision;
    }

    private void OnHeadBounceCollision(HeadBounceTarget headBounceTarget)
    {
        headBounceTarget.GetComponent<HealthController>().RemoveHealth(1);
    }
}