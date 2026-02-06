using UnityEngine;

[RequireComponent(typeof(WaterDetector))]
public class SplashParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _splashParticleSystem;

    private WaterDetector _waterDetector;

    private void Awake()
    {
        _waterDetector = GetComponent<WaterDetector>();
        _waterDetector.OnSplashEntry += OnSplashEntry;
    }

    private void OnSplashEntry()
    {
        _splashParticleSystem.Play();
    }
}