public class EnemyHeadBounceStateTransition : StateTransition
{
    private readonly HeadBounceCollisionController _headBounceCollisionController;
    private bool _headBounceCollisionOccurred;

    public EnemyHeadBounceStateTransition(State targetState, float duration, HeadBounceCollisionController headBounceCollisionController)
        : base(targetState, duration)
    {
        _headBounceCollisionController = headBounceCollisionController;
    }

    public override void SetUp()
    {
        _headBounceCollisionController.OnHeadBounceCollision += OnHeadBounceCollision;
    }

    public override bool IsSatisfied()
    {
        return _headBounceCollisionOccurred;
    }

    public override void TearDown()
    {
        _headBounceCollisionController.OnHeadBounceCollision -= OnHeadBounceCollision;
        _headBounceCollisionOccurred = false;
    }

    private void OnHeadBounceCollision(HeadBounceTarget headBounceTarget)
    {
        _headBounceCollisionOccurred = true;
    }
}
