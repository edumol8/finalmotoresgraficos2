using UnityEngine;

public class FootstepParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _leftFootParticleSystem;

    [SerializeField]
    private ParticleSystem _rightFootParticleSystem;

    private FootstepController _footstepController;

    private void Awake()
    {
        _footstepController = GetComponent<FootstepController>();
    }

    private void Start()
    {
        _footstepController.OnLeftFootDown += PlayLeftFootParticleSystem;
        _footstepController.OnRightFootDown += PlayRightFootParticleSystem;
    }

    private void PlayLeftFootParticleSystem()
    {
        if (enabled)
        {
            _leftFootParticleSystem.Play();
        }
    }

    private void PlayRightFootParticleSystem()
    {
        if (enabled)
        {
            _rightFootParticleSystem.Play();
        }
    }

    private void OnDisable()
    {
        _leftFootParticleSystem.Stop();
        _rightFootParticleSystem.Stop();
    }
}