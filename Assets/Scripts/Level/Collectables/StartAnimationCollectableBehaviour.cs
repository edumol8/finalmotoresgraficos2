using UnityEngine;

public class StartAnimationCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField]
    private string _animatorStateName;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnCollected(GameObject player)
    {
        _animator.CrossFadeInFixedTime(_animatorStateName, 0, 0);
    }
}
