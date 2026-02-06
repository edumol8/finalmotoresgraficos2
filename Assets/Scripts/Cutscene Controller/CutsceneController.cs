using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    public bool IsCutscenePlaying { get; private set; }

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.stopped += CutsceneStopped;
    }

    private void CutsceneStopped(PlayableDirector playableDirector)
    {
        IsCutscenePlaying = false;
    }

    public void PlayCutscene(TimelineAsset cutscene)
    {
        _playableDirector.playableAsset = cutscene;
        _playableDirector.Play();
        IsCutscenePlaying = true;
    }
}
