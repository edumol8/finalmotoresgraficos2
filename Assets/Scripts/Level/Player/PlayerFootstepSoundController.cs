using UnityEngine;

public class PlayerFootstepSoundController : MonoBehaviour
{
    [SerializeField]
    private RandomPitchAudioClip _footstepAudioClip;

    private AudioSource _audioSource;
    private FootstepController _footstepController;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _footstepController = GetComponent<FootstepController>();
    }

    private void Start()
    {
        _footstepController.OnLeftFootDown += PlayFootstepSound;
        _footstepController.OnRightFootDown += PlayFootstepSound;
    }

    private void PlayFootstepSound()
    {
        _audioSource.PlayOneShot(_footstepAudioClip);
    }
}