using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private readonly List<StateTransition> _anyStateTransitions = new List<StateTransition>();

    private State _currentState;

    public void AddAnyStateTransition(StateTransition stateTransition)
    {
        stateTransition.SetUp();
        _anyStateTransitions.Add(stateTransition);
    }

    private void Update()
    {
        _currentState?.Update();
    }

    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }

    private void LateUpdate()
    {
        var satisfiedTransition = GetSatisfiedTransition();

        if (satisfiedTransition != null)
        {
            SwitchState(satisfiedTransition.TargetState, satisfiedTransition.Duration);
        }
    }

    private StateTransition GetSatisfiedTransition()
    {
        foreach (var anyStateTransition in _anyStateTransitions)
        {
            if (anyStateTransition.TargetState != _currentState && anyStateTransition.IsSatisfied())
            {
                anyStateTransition.TearDown();
                anyStateTransition.SetUp();

                return anyStateTransition;
            }
        }

        return _currentState?.GetSatisfiedTransition();
    }

    public void SwitchState(State newState, float transitionDuration)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(transitionDuration);
    }
}
