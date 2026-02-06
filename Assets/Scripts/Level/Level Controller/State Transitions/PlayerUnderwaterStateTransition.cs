public class PlayerUnderwaterStateTransition : StateTransition
{
    private readonly WaterDetector _underwaterDetector;

    private bool _isUnderwater;

    public PlayerUnderwaterStateTransition(
        State targetState,
        float duration,
        WaterDetector underwaterDetector)
        : base(targetState, duration)
    {
        _underwaterDetector = underwaterDetector;
    }

    public override void SetUp()
    {
        _underwaterDetector.OnUnderwater += OnUnderwater;
    }

    private void OnUnderwater()
    {
        _isUnderwater = true;
    }

    public override bool IsSatisfied()
    {
        return _isUnderwater;
    }

    public override void TearDown()
    {
        _underwaterDetector.OnUnderwater -= OnUnderwater;
        _isUnderwater = false;
    }
}
