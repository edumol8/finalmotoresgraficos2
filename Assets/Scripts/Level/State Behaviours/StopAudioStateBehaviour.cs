using UnityEngine;
using Unity;

public class StopAudioClipStateBehaviour : StateBehaviour
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _audioClip;

    public StopAudioClipStateBehaviour(AudioSource audioSource, AudioClip audioClip)
    {
        _audioSource = audioSource;
        _audioClip = audioClip;
    }

    public override void Enter(float transitionDuration)
    {
        if (_audioSource.clip == _audioClip)
        {
            _audioSource.Stop();
        }
    }
}

