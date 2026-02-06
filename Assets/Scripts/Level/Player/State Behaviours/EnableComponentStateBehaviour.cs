using UnityEngine;

public class EnableComponentStateBehaviour : StateBehaviour
{
    private readonly MonoBehaviour _component;

    public EnableComponentStateBehaviour(MonoBehaviour component)
    {
        _component = component;
    }

    public override void Enter(float transitionDuration)
    {
        _component.enabled = true;
    }
}