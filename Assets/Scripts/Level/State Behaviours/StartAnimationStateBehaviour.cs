using UnityEngine;

public class StartAnimationStateBehaviour : StateBehaviour
{
    private readonly Animator _animator;
    private readonly string _animatorStateName;

    public StartAnimationStateBehaviour(Animator animator, string animatorStateName)
    {
        _animator = animator;
        _animatorStateName = animatorStateName;
    }

    public override void Enter(float transitionDuration)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(_animatorStateName) == false
            || _animator.IsInTransition(0))
        {
            _animator.CrossFadeInFixedTime(_animatorStateName, transitionDuration, 0);
        }
    }
}
