using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaysoundCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField]
    private RandomPitchAudioClip _randomPitchAudioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnCollected(GameObject player)
    {
        _audioSource.PlayOneShot(_randomPitchAudioClip);
    }
}
