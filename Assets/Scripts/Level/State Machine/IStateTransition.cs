public abstract class StateTransition
{
    protected StateTransition(State targetState, float duration)
    {
        TargetState = targetState;
        Duration = duration;
    }

    public State TargetState { get; }

    public float Duration { get; }

    public virtual void SetUp()
    {
    }

    public virtual bool IsSatisfied()
    {
        return false;
    }

    public virtual void TearDown()
    {
    }
}
