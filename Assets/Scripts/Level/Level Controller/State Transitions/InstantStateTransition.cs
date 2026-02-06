public class InstantStateTransition : StateTransition
{
    public InstantStateTransition(State targetState, float duration)
        : base(targetState, duration)
    {
    }

    public override bool IsSatisfied()
    {
        return true;
    }
}