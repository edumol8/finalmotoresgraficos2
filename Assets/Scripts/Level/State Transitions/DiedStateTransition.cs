public class DiedStateTransition : StateTransition
{
    private readonly HealthController _healthController;

    private bool _isDead;

    public DiedStateTransition(
        State targetState,
        float duration,
        HealthController healthController)
        : base(targetState, duration)
    {
        _healthController = healthController;
    }

    public override void SetUp()
    {
        _healthController.OnDied += OnDied;
    }

    private void OnDied()
    {
        _isDead = true;
    }

    public override bool IsSatisfied()
    {
        return _isDead;
    }

    public override void TearDown()
    {
        _healthController.OnDied -= OnDied;
        _isDead = false;
    }
}
