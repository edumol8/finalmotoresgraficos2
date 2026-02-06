using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System;

public class SceneFadeMasterAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private SceneController _sceneController;

    private string masterVolumeParameter = "Master Volume";

    private void Awake()
    {
        _sceneController.OnSceneFadingOut += FadeOutAudio;
        _audioMixer.SetFloat(masterVolumeParameter, 0);
    }

    private void FadeOutAudio(SceneFadingOutEventArgs eventArgs)
    {
        StartCoroutine(FadeOutCoroutine(eventArgs.FadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        float startVolume;
        _audioMixer.GetFloat(masterVolumeParameter, out startVolume);

        float elapsedTime = 0;
        float elapsedPercentage = 0;
        bool firstFrame = true;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / fadeDuration;
            float newVolume = Mathf.Lerp(startVolume, -80f, elapsedPercentage);

            _audioMixer.SetFloat(masterVolumeParameter, newVolume);

            yield return null;

            if (firstFrame == false)
            {
                elapsedTime += Time.unscaledDeltaTime;
            }

            firstFrame = false;
        }

        _audioMixer.SetFloat(masterVolumeParameter, -80f);
    }
}
