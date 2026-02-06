using UnityEngine;
using UnityEngine.AI;

public class PlayerAwarenessController : MonoBehaviour
{
    [SerializeField]
    private float _playerAwarenessDistance;

    [SerializeField]
    private GameObjectReference _playerGameObjectReference;

    public bool AwareOfPlayer { get; private set; }

    public NavMeshPath PathToPlayer { get; private set; }
    
    private float _playerAwarenessSqrDistance;
    
    private NavMeshAgent _navMeshAgent;

    private GroundController _playerGroundController;

    private Vector3? _lastValidPlayerPosition;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        PathToPlayer = new NavMeshPath();

        _playerAwarenessSqrDistance = _playerAwarenessDistance * _playerAwarenessDistance;
    }

    void Update()
    {
        AwareOfPlayer = false;

        var playerGroundController = GetPlayerGroundController();

        if (playerGroundController == null || playerGroundController.GroundPosition == null)
        {
            return;
        }

        Vector3 enemyToPlayerVector = playerGroundController.GroundPosition.Value - transform.position;

        if (enemyToPlayerVector.sqrMagnitude <= _playerAwarenessSqrDistance)
        {
            if (_navMeshAgent.CalculatePath(playerGroundController.GroundPosition.Value, PathToPlayer) && PathToPlayer.status == NavMeshPathStatus.PathComplete)
            {
                _lastValidPlayerPosition = playerGroundController.GroundPosition.Value;
                AwareOfPlayer = true;
            }
            else if (_lastValidPlayerPosition.HasValue)
            {
                if (_navMeshAgent.velocity != Vector3.zero)
                {
                    // Carry on going to the last valid position of the player so there isn't a sudden stop
                    _navMeshAgent.CalculatePath(_lastValidPlayerPosition.Value, PathToPlayer);
                    AwareOfPlayer = true;
                }
                else
                {
                    _lastValidPlayerPosition = null;
                }
            }
        }
    }

    private GroundController GetPlayerGroundController()
    {
        if (_playerGroundController == null)
        {
            if (_playerGameObjectReference.GameObject != null)
            {
                _playerGroundController = _playerGameObjectReference.GameObject.GetComponent<GroundController>();
            }
        }

        return _playerGroundController;
    }
}
