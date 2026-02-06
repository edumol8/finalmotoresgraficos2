using UnityEngine.AI;

public class ReachedDestinationStateTransition : StateTransition
{
    private NavMeshAgent _navMeshAgent;

    public ReachedDestinationStateTransition(State targetState, float duration, NavMeshAgent navMeshAgent)
        : base(targetState, duration)
    {
        _navMeshAgent = navMeshAgent;
    }

    public override bool IsSatisfied()
    {
        return _navMeshAgent.remainingDistance <= 0.1f;
    }
}