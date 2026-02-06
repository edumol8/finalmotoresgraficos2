using System.Collections;
using System.Linq;
using UnityEngine;

public class PlaylistController : MonoBehaviour
{
    [SerializeField]
    private Playlist _playlist;

    private AudioSource _audioSource;
    private int? _currentClipIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioSource.isPlaying)
        {
            return;
        }

        PlayNextPlaylistClip();
    }

    private void PlayNextPlaylistClip()
    {
        if (_playlist != null && _playlist.Clips.Any())
        {
            _currentClipIndex = _currentClipIndex.HasValue ? _currentClipIndex + 1 : 0;

            if (_currentClipIndex >= _playlist.Clips.Count())
            {
                _currentClipIndex = 0;
            }

            _audioSource.clip = _playlist.Clips.ElementAt(_currentClipIndex.Value);
            _audioSource.Play();
        }
    }

    public void FadeVolumeOut(float duration)
    {
        StartCoroutine(ChangeVolumeCoroutine(duration, 1, 0));
    }

    public void FadeVolumeIn(float duration)
    {
        StartCoroutine(ChangeVolumeCoroutine(duration, 0, 1));
    }

    private IEnumerator ChangeVolumeCoroutine(float duration, float startVolume, float targetVolume)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;
        bool firstFrame = true;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;

            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedPercentage);

            yield return null;

            if (firstFrame == false)
            {
                elapsedTime += Time.unscaledDeltaTime;
            }

            firstFrame = false;
        }

        _audioSource.volume = targetVolume;
    }
}
