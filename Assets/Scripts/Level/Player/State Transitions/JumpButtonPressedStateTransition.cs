public class JumpButtonPressedStateTransition : StateTransition
{
    private PlayerInputController _playerInputController;
    private bool _jumpButtonPressed;

    public JumpButtonPressedStateTransition(
        State targetState,
        float duration,
        PlayerInputController playerInputController)
        : base(targetState, duration)
    {
        _playerInputController = playerInputController;
    }

    public override void SetUp()
    {
        _jumpButtonPressed = false;
        _playerInputController.OnJumpButtonPressed += JumpButtonPressed;
    }

    public override bool IsSatisfied()
    {
        return _jumpButtonPressed;
    }

    public override void TearDown()
    {
        _playerInputController.OnJumpButtonPressed -= JumpButtonPressed;
    }

    private void JumpButtonPressed()
    {
        _jumpButtonPressed = true;
    }
}
