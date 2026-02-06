using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayCutsceneStateBehaviour : StateBehaviour
{
    private readonly CutsceneController _cutsceneController;
    private readonly TimelineAsset _playableAsset;

    public PlayCutsceneStateBehaviour(CutsceneController cutsceneController, TimelineAsset playableAsset)
    {
        _cutsceneController = cutsceneController;
        _playableAsset = playableAsset;
    }

    public override void Enter(float transitionDuration)
    {
        _cutsceneController.PlayCutscene(_playableAsset);
    }
}
