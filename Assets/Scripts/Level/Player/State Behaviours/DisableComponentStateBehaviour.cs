using UnityEditor;
using UnityEngine;

public class DisableComponentStateBehaviour : StateBehaviour
{
    private readonly MonoBehaviour _component;

    public DisableComponentStateBehaviour(MonoBehaviour component)
    {
        _component = component;
    }

    public override void Enter(float transitionDuration)
    {
        _component.enabled = false;
    }
}
