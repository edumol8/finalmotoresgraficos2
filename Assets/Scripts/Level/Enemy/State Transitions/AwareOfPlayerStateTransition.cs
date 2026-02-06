public class AwareOfPlayerStateTransition : StateTransition
{
    private readonly PlayerAwarenessController _playerAwarenessController;

    public AwareOfPlayerStateTransition(
        State targetState,
        float duration,
        PlayerAwarenessController playerAwarenessController)
        : base(targetState, duration)
    {
        _playerAwarenessController = playerAwarenessController;
    }

    public override bool IsSatisfied()
    {
        return _playerAwarenessController.AwareOfPlayer;
    }
}
