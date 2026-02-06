public class CutscenePlayingStateTransition : StateTransition
{
    private readonly CutsceneController _cutsceneContreoller;

    public CutscenePlayingStateTransition(State targetState, float duration, CutsceneController cutsceneContreoller)
        : base(targetState, duration)
    {
        _cutsceneContreoller = cutsceneContreoller;
    }

    public override bool IsSatisfied()
    {
        return _cutsceneContreoller.IsCutscenePlaying;
    }
}
