public class EndPortalReachedStateTransition : StateTransition
{
    private readonly EndPortalDetector _endPortalDetector;
    private bool _endPortalReached;

    public EndPortalReachedStateTransition(State targetState, float duration, EndPortalDetector underwaterDetector)
        : base(targetState, duration)
    {
        _endPortalDetector = underwaterDetector;
    }

    public override void SetUp()
    {
        _endPortalDetector.OnEndPortalReached += OnEndPortalReached;
    }

    public override bool IsSatisfied()
    {
        return _endPortalReached;
    }

    public override void TearDown()
    {
        _endPortalReached = false;
        _endPortalDetector.OnEndPortalReached -= OnEndPortalReached;
    }

    private void OnEndPortalReached()
    {
        _endPortalReached = true;
    }
}
