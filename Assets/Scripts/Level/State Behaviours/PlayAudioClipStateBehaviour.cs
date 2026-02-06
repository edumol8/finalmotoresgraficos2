using UnityEngine;

public class PlayAudioClipStateBehaviour : StateBehaviour
{
    private readonly AudioSource _audioSource;
    private readonly RandomPitchAudioClip _audioClip;
    private readonly float _delay;

    private bool _clipPlayed;
    private float _delayCountdown;

    public PlayAudioClipStateBehaviour(AudioSource audioSource, RandomPitchAudioClip audioClip, float delay)
    {
        _audioSource = audioSource;
        _audioClip = audioClip;
        _delay = delay;
    }

    public override void Enter(float transitionDuration)
    {
        _delayCountdown = _delay;
        _clipPlayed = false;
    }

    public override void Update()
    {
        _delayCountdown -= Time.deltaTime;

        if (_delayCountdown <= 0 && _clipPlayed == false)
        {
            _audioSource.PlayOneShot(_audioClip);
            _clipPlayed = true;
        }
    }
}

