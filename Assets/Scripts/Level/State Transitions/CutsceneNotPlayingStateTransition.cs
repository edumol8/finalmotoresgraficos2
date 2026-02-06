public class CutsceneNotPlayingStateTransition : StateTransition
{
    private readonly CutsceneController _cutsceneController;

    public CutsceneNotPlayingStateTransition(State targetState, float duration, CutsceneController cutsceneController)
        : base(targetState, duration)
    {
        _cutsceneController = cutsceneController;
    }

    public override bool IsSatisfied()
    {
        return _cutsceneController.IsCutscenePlaying == false;
    }
}