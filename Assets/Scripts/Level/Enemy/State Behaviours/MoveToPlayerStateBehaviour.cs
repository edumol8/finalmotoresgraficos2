using UnityEngine.AI;
using UnityEngine;

public class MoveToPlayerStateBehaviour : StateBehaviour
{
    private PlayerAwarenessController _playerAwarenessController;
    private NavMeshAgent _navMeshAgent;
    private bool _collidingWithPlayer;
    private readonly CollisionController _collisionController;

    public MoveToPlayerStateBehaviour(
        PlayerAwarenessController playerAwarenessController,
        NavMeshAgent navMeshAgent,
        CollisionController collisionController)
    {
        _playerAwarenessController = playerAwarenessController;
        _navMeshAgent = navMeshAgent;
        _collisionController = collisionController;
    }
    public override void Enter(float transitionDuration)
    {
        _navMeshAgent.avoidancePriority = Random.Range(30, 60);
        _collisionController.OnEnterCollision += OnEnterCollision;
        _collisionController.OnExitCollision += OnExitCollision;
    }

    public override void Exit()
    {
        _navMeshAgent.ResetPath();
        _collisionController.OnEnterCollision -= OnEnterCollision;
        _collisionController.OnExitCollision -= OnExitCollision;
    }

    public override void Update()
    {
        _navMeshAgent.ResetPath();

        if (_collidingWithPlayer == false)
        { 
            _navMeshAgent.SetPath(_playerAwarenessController.PathToPlayer);
        }
    }

    private void OnEnterCollision(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            _collidingWithPlayer = true;
        }
    }

    private void OnExitCollision(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            _collidingWithPlayer = false;
        }
    }
}
