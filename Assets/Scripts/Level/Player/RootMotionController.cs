using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    [SerializeField]
    private CutsceneController _custsceneController;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        if (_custsceneController.IsCutscenePlaying)
        {
            transform.position += _animator.deltaPosition;
            Physics.SyncTransforms();
        }
    }
}

