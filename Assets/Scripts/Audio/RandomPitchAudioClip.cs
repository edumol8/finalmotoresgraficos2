using UnityEngine;

[CreateAssetMenu(fileName = "Audio Clip", menuName = "Scriptable Objects/Random Pitch Audio Clip", order = 1)]
public class RandomPitchAudioClip : ScriptableObject
{
    [field: SerializeField]
    public AudioClip AudioClip { get; private set; }

    [field: SerializeField]
    public float MinimumPitch { get; private set; } = 1f;

    [field: SerializeField]
    public float MaximumPitch { get; private set; } = 1f;

}
