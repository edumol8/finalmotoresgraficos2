using UnityEngine;
using Unity;

public class PlayLoopedAudioClipStateBehaviour : StateBehaviour
{
    private readonly AudioSource _audioSource;
    private readonly AudioClip _audioClip;
    private readonly float _maximumDelay;
    private float _elapsedTime;
    private float _delay;
    private bool _soundPlaying;

    public PlayLoopedAudioClipStateBehaviour(AudioSource audioSource, AudioClip audioClip, float maximumDelay)
    {
        _audioSource = audioSource;
        _audioClip = audioClip;
        _maximumDelay = maximumDelay;
    }

    public override void Enter(float transitionDuration)
    {
        _elapsedTime = 0;
        _delay = Random.Range(0f, _maximumDelay);
    }

    public override void Update()
    {
        if (_soundPlaying)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _delay)
        {
            _audioSource.loop = true;
            _audioSource.clip = _audioClip;
            _audioSource.Play();
            _soundPlaying = true;
        }
    }

    public override void Exit()
    {
        _audioSource.loop = false;
        _soundPlaying = false;
    }
}