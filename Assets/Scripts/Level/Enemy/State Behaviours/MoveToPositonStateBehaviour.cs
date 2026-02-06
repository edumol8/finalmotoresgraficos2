using UnityEngine.AI;
using UnityEngine;

public class MoveToPositonStateBehaviour : StateBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Vector3 _targetPosition;

    public MoveToPositonStateBehaviour(NavMeshAgent navMeshAgent, Vector3 position)
    {
        _navMeshAgent = navMeshAgent;
        _targetPosition = position;
    }

    public override void Enter(float transitionDuration)
    {
        _navMeshAgent.SetDestination(_targetPosition);
    }
}