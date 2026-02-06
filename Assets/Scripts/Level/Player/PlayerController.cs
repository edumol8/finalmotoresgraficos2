using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraTransform;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _jumpSpeed;

    [SerializeField]
    private float _doubleJumpSpeed;

    [SerializeField]
    private float _headBounceSpeed;

    [SerializeField]
    private float _coyoteTime;

    [SerializeField]
    private float _maximumHorizontalJumpSpeed;

    [SerializeField]
    private PhysicsMaterial _movingPhysicMaterial;

    [SerializeField]
    private PhysicsMaterial _stationaryPhysicMaterial;

    [SerializeField]
    private CutsceneController _cutsceneController;

    [SerializeField]
    private RandomPitchAudioClip _jumpAudioClip;

    [SerializeField]
    private RandomPitchAudioClip _doubleJumpAudioClip;

    [SerializeField]
    private RandomPitchAudioClip _deathAudioClip;

    private void Start()
    {
        var stateMachine = GetComponent<StateMachine>();

        var animator = GetComponent<Animator>();
        var rigidbody = GetComponent<Rigidbody>();
        var collider = GetComponent<Collider>();
        var playerInputController = GetComponent<PlayerInputController>();
        var groundController = GetComponent<GroundController>();
        var headBounceCollisionController = GetComponent<HeadBounceCollisionController>();
        var collisionController = GetComponent<CollisionController>();
        var healthController = GetComponent<HealthController>();
        var gravityController = GetComponent<GravityController>();
        var underwaterDetector = GetComponent<WaterDetector>();
        var footstepParticleController = GetComponent<FootstepParticleController>();
        var audioSource = GetComponent<AudioSource>();

        var idleState = new State();
        var locomotionState = new State();
        var jumpAscentState = new State();
        var jumpDescentState = new State();
        var landState = new State();
        var fallState = new State();
        var doubleJumpAscentState = new State();
        var enemyHeadBounceState = new State();
        var deadState = new State();
        var drownState = new State();
        var cutsceneState = new State();

        var stopMovementStateBehaviour = new StopMovementStateBehaviour(rigidbody, collider, _stationaryPhysicMaterial, _movingPhysicMaterial);
        var rotateToDirectionOfInputStateBehaviour = new RotateToDirectionOfInputStateBehaviour(playerInputController, rigidbody, _cameraTransform, _rotationSpeed);
        var collectCollectablesStateBehaviour = new CollectCollectablesStateBehaviour(collisionController);
        var headBounceAttackStateBehaviour = new HeadBounceAttackStateBehaviour(headBounceCollisionController);
        var enableFootstepParticlesStateBehaviour = new EnableComponentStateBehaviour(footstepParticleController);
        var disableFootstepParticlesStateBehaviour = new DisableComponentStateBehaviour(footstepParticleController);

        var jumpButtonPressedStateTransition = new JumpButtonPressedStateTransition(jumpAscentState, 0.1f, playerInputController);
        var doubleJumpButtonPressedStateTransition = new JumpButtonPressedStateTransition(doubleJumpAscentState, 0.1f, playerInputController);
        var isFallingStateTransition = new IsFallingStateTransition(fallState, 0.2f, groundController, _coyoteTime, 0.5f);
        var isJumpDescendingStateTransition = new IsFallingStateTransition(jumpDescentState, 0.2f, groundController, _coyoteTime, 0.5f);
        var enemyHeadBounceStateTransition = new EnemyHeadBounceStateTransition(enemyHeadBounceState, 0.1f, headBounceCollisionController);

        idleState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Idle"));
        idleState.AddStateBehaviour(stopMovementStateBehaviour);
        idleState.AddStateBehaviour(collectCollectablesStateBehaviour);
        idleState.AddStateBehaviour(disableFootstepParticlesStateBehaviour);
        idleState.AddStateBehaviour(headBounceAttackStateBehaviour);
        idleState.AddStateTransition(new HasMovementInputStateTransition(locomotionState, 0.2f, playerInputController, 0.1f));
        idleState.AddStateTransition(jumpButtonPressedStateTransition);
        idleState.AddStateTransition(isFallingStateTransition);
        idleState.AddStateTransition(enemyHeadBounceStateTransition);

        locomotionState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Locomotion"));
        locomotionState.AddStateBehaviour(new LocomotionStateBehaviour(animator, playerInputController, rigidbody, _cameraTransform));
        locomotionState.AddStateBehaviour(rotateToDirectionOfInputStateBehaviour);
        locomotionState.AddStateBehaviour(collectCollectablesStateBehaviour);
        locomotionState.AddStateBehaviour(enableFootstepParticlesStateBehaviour);
        locomotionState.AddStateBehaviour(headBounceAttackStateBehaviour);
        locomotionState.AddStateTransition(new NoMovementInputStateTransition(idleState, 0.2f, playerInputController, 0.1f));
        locomotionState.AddStateTransition(jumpButtonPressedStateTransition);
        locomotionState.AddStateTransition(isFallingStateTransition);
        locomotionState.AddStateTransition(enemyHeadBounceStateTransition);

        SetupAirborneState(
            jumpAscentState,
            landState,
            rigidbody,
            playerInputController,
            rotateToDirectionOfInputStateBehaviour,
            collectCollectablesStateBehaviour,
            headBounceAttackStateBehaviour,
            disableFootstepParticlesStateBehaviour,
            enemyHeadBounceStateTransition,
            groundController);

        jumpAscentState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Jump"));
        jumpAscentState.AddStateBehaviour(new JumpStateBehaviour(rigidbody, _jumpSpeed));
        jumpAscentState.AddStateBehaviour(new PlayAudioClipStateBehaviour(audioSource, _jumpAudioClip, 0f));
        jumpAscentState.AddStateTransition(isJumpDescendingStateTransition);
        jumpAscentState.AddStateTransition(doubleJumpButtonPressedStateTransition);

        SetupAirborneState(
            enemyHeadBounceState,
            landState,
            rigidbody,
            playerInputController,
            rotateToDirectionOfInputStateBehaviour,
            collectCollectablesStateBehaviour,
            headBounceAttackStateBehaviour,
            disableFootstepParticlesStateBehaviour,
            enemyHeadBounceStateTransition,
            groundController);

        enemyHeadBounceState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Jump"));
        enemyHeadBounceState.AddStateBehaviour(new JumpStateBehaviour(rigidbody, _headBounceSpeed));
        enemyHeadBounceState.AddStateTransition(isJumpDescendingStateTransition);
        enemyHeadBounceState.AddStateTransition(doubleJumpButtonPressedStateTransition);

        SetupAirborneState(
            doubleJumpAscentState,
            landState,
            rigidbody,
            playerInputController,
            rotateToDirectionOfInputStateBehaviour,
            collectCollectablesStateBehaviour,
            headBounceAttackStateBehaviour,
            disableFootstepParticlesStateBehaviour,
            enemyHeadBounceStateTransition,
            groundController);

        doubleJumpAscentState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Flip"));
        doubleJumpAscentState.AddStateBehaviour(new JumpStateBehaviour(rigidbody, _doubleJumpSpeed));
        doubleJumpAscentState.AddStateBehaviour(new PlayAudioClipStateBehaviour(audioSource, _doubleJumpAudioClip, 0f));
        doubleJumpAscentState.AddStateTransition(new ElapsedTimeStateTransition(fallState, 0.25f, 0.45f));

        SetupAirborneState(
            fallState,
            landState,
            rigidbody,
            playerInputController,
            rotateToDirectionOfInputStateBehaviour,
            collectCollectablesStateBehaviour,
            headBounceAttackStateBehaviour,
            disableFootstepParticlesStateBehaviour,
            enemyHeadBounceStateTransition,
            groundController);

        fallState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Fall"));

        SetupAirborneState(
            jumpDescentState,
            landState,
            rigidbody,
            playerInputController,
            rotateToDirectionOfInputStateBehaviour,
            collectCollectablesStateBehaviour,
            headBounceAttackStateBehaviour,
            disableFootstepParticlesStateBehaviour,
            enemyHeadBounceStateTransition,
            groundController);

        jumpDescentState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Fall"));
        jumpDescentState.AddStateTransition(doubleJumpButtonPressedStateTransition);

        landState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Land"));
        landState.AddStateBehaviour(stopMovementStateBehaviour);
        landState.AddStateBehaviour(collectCollectablesStateBehaviour);
        landState.AddStateBehaviour(headBounceAttackStateBehaviour);
        landState.AddStateBehaviour(enableFootstepParticlesStateBehaviour);
        landState.AddStateTransition(new LandingCompleteStateTransition(idleState, 0.2f, playerInputController, 0.8f, 0, 0.1f));
        landState.AddStateTransition(new LandingCompleteStateTransition(locomotionState, 0.2f, playerInputController, 0.2f, 0.1f, 1f));
        landState.AddStateTransition(jumpButtonPressedStateTransition);
        landState.AddStateTransition(enemyHeadBounceStateTransition);

        deadState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Die"));
        deadState.AddStateBehaviour(new PlayAudioClipStateBehaviour(audioSource, _deathAudioClip, 0));
        deadState.AddStateBehaviour(stopMovementStateBehaviour);

        drownState.AddStateBehaviour(new StartAnimationStateBehaviour(animator, "Drown"));
        drownState.AddStateBehaviour(stopMovementStateBehaviour);
        drownState.AddStateBehaviour(new DrownStateBehaviour(gravityController, rigidbody, underwaterDetector));

        cutsceneState.AddStateBehaviour(stopMovementStateBehaviour);
        cutsceneState.AddStateTransition(new CutsceneNotPlayingStateTransition(idleState, 0.2f, _cutsceneController));

        stateMachine.AddAnyStateTransition(new DiedStateTransition(deadState, 0.2f, healthController));
        stateMachine.AddAnyStateTransition(new PlayerUnderwaterStateTransition(drownState, 0.2f, underwaterDetector));
        stateMachine.AddAnyStateTransition(new CutscenePlayingStateTransition(cutsceneState, 0, _cutsceneController));

        stateMachine.SwitchState(idleState, 0);
    }

    private void SetupAirborneState(
        State airborneState,
        State landState,
        Rigidbody rigidbody, 
        PlayerInputController playerInputController,
        RotateToDirectionOfInputStateBehaviour rotateToDirectionOfInputStateBehaviour,
        CollectCollectablesStateBehaviour collectCollectablesStateBehaviour,
        HeadBounceAttackStateBehaviour headBounceAttackStateBehaviour,
        DisableComponentStateBehaviour disableFootstepParticlesStateBehaviour,
        EnemyHeadBounceStateTransition enemyHeadBounceStateTransition,        
        GroundController groundController)
    {
        airborneState.AddStateBehaviour(new AirMovementStateBehaviour(playerInputController, _cameraTransform, _maximumHorizontalJumpSpeed, rigidbody));
        airborneState.AddStateBehaviour(rotateToDirectionOfInputStateBehaviour);
        airborneState.AddStateBehaviour(headBounceAttackStateBehaviour);
        airborneState.AddStateBehaviour(collectCollectablesStateBehaviour);
        airborneState.AddStateBehaviour(disableFootstepParticlesStateBehaviour);
        airborneState.AddStateTransition(enemyHeadBounceStateTransition);
        airborneState.AddStateTransition(new IsGroundedStateTransition(landState, 0.1f, groundController, rigidbody));
    }
}
