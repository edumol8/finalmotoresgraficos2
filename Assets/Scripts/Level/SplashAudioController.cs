using UnityEngine;

[RequireComponent(typeof(WaterDetector), typeof(AudioSource))]
public class SplashAudioController : MonoBehaviour
{
    [SerializeField]
    private RandomPitchAudioClip _splashAudioClip;

    private WaterDetector _waterDetector;
    private AudioSource _audioSource;

    private void Awake()
    {
        _waterDetector = GetComponent<WaterDetector>();
        _audioSource = GetComponent<AudioSource>();
        _waterDetector.OnSplashEntry += OnSplashEntry;
    }

    private void OnSplashEntry()
    {
        _audioSource.PlayOneShot(_splashAudioClip);
    }
}
