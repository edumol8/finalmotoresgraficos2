using UnityEngine;

public class DamagedAudioController : MonoBehaviour
{
    [SerializeField]
    private RandomPitchAudioClip _damagedAudioClip;

    private HealthController _healthController;
    private AudioSource _audioSource;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _audioSource = GetComponent<AudioSource>();
        _healthController.OnDamaged += OnDamaged;
    }

    private void OnDamaged()
    {
        _audioSource.PlayOneShot(_damagedAudioClip);
    }
}
