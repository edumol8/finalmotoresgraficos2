using UnityEngine;

public class LogEnemySoundController : MonoBehaviour
{
    [SerializeField]
    private AudioClip _deathAudioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound()
    {
        _audioSource.PlayOneShot(_deathAudioClip);
    }
}
