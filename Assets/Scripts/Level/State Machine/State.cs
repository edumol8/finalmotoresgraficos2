using System;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    private List<StateBehaviour> _stateBehaviours = new List<StateBehaviour>();

    private List<StateTransition> _stateTransitions = new List<StateTransition>();

    public void AddStateBehaviour(StateBehaviour stateBehaviour)
    {
        _stateBehaviours.Add(stateBehaviour);
    }

    public void AddStateTransition(StateTransition stateTransition)
    {
        _stateTransitions.Add(stateTransition);
    }

    public void Enter(float transitionDuration)
    {
        foreach (var stateBehaviour in _stateBehaviours)
        {
            stateBehaviour.Enter(transitionDuration);
        }

        foreach (var stateTransition in _stateTransitions)
        {
            stateTransition.SetUp();
        }
    }

    public void Update()
    {
        foreach (var stateBehaviour in _stateBehaviours)
        {
            stateBehaviour.Update();
        }
    }

    public void FixedUpdate()
    {
        foreach (var stateBehaviour in _stateBehaviours)
        {
            stateBehaviour.FixedUpdate();
        }
    }

    public void Exit()
    {
        foreach (var stateBehaviour in _stateBehaviours)
        {
            stateBehaviour.Exit();
        }

        foreach (var stateTransition in _stateTransitions)
        {
            stateTransition.TearDown();
        }
    }

    public StateTransition GetSatisfiedTransition()
    {
        foreach (var stateTransition in _stateTransitions)
        {
            if (stateTransition.IsSatisfied())
            {
                return stateTransition;
            }
        }

        return null;
    }
}