using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtension
{
    public static void PlayOneShot(this AudioSource audioSource, RandomPitchAudioClip randomPitchAudioClip)
    {
        float randomPitch = Random.Range(randomPitchAudioClip.MinimumPitch, randomPitchAudioClip.MaximumPitch);
        audioSource.pitch = randomPitch;

        audioSource.PlayOneShot(randomPitchAudioClip.AudioClip);
    }
}
