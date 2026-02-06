using UnityEngine;
using UnityEngine.AI;

public class LogEnemyController : MonoBehaviour
{
    [SerializeField]
    private RandomPitchAudioClip _hitAudioClip;

    [SerializeField]
    private RandomPitchAudioClip _explodeAudioClip;

    [SerializeField]
    private AudioClip _chaseAudioClip;

    [SerializeField]
    private GameObjectReference _playerGameObjectReference;

    private void Start()
    {
        var stateMachine = GetComponent<StateMachine>();
        var playerAwarenessController = GetComponent<PlayerAwarenessController>();
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        var collisionController = GetComponent<CollisionController>();
        var healthController = GetComponent<HealthController>();
        var audioSource = GetComponent<AudioSource>();

        var idleState = new State();
        State chaseState = new State();
        State lostPlayerState = new State();
        State returnToStartState = new State();
        State deadState = new State();

        AwareOfPlayerStateTransition awareOfPlayerStateTransition = new AwareOfPlayerStateTransition(chaseState, 0.2f, playerAwarenessController);

        idleState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Idle"));
        idleState.AddStateTransition(awareOfPlayerStateTransition);

        chaseState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Run"));
        chaseState.AddStateBehaviour(new PlayLoopedAudioClipStateBehaviour(audioSource, _chaseAudioClip, 1f));
        chaseState.AddStateBehaviour(new MoveToPlayerStateBehaviour(playerAwarenessController, navMeshAgent, collisionController));
        chaseState.AddStateTransition(new UnawareOfPlayerStateTransition(lostPlayerState, 0.2f, playerAwarenessController));

        lostPlayerState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Idle"));
        lostPlayerState.AddStateTransition(new ElapsedTimeStateTransition(returnToStartState, 0.2f, 5f));
        lostPlayerState.AddStateTransition(awareOfPlayerStateTransition);

        returnToStartState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Run"));
        returnToStartState.AddStateBehaviour(new MoveToPositonStateBehaviour(navMeshAgent, transform.position));
        returnToStartState.AddStateTransition(awareOfPlayerStateTransition);
        returnToStartState.AddStateTransition(new ReachedDestinationStateTransition(idleState, 0.2f, navMeshAgent));

        deadState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Death"));
        deadState.AddStateBehaviour(new StopAudioClipStateBehaviour(audioSource, _chaseAudioClip));
        deadState.AddStateBehaviour(new PlayAudioClipStateBehaviour(audioSource, _hitAudioClip, 0));
        deadState.AddStateBehaviour(new PlayAudioClipStateBehaviour(audioSource, _explodeAudioClip, 0.1f));

        stateMachine.AddAnyStateTransition(new DiedStateTransition(deadState, 0.2f, healthController));

        stateMachine.SwitchState(idleState, 0);
    }
}
