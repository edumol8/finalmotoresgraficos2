using UnityEngine;

public class ElapsedTimeStateTransition : StateTransition
{
    private readonly float _elapsedTimeRequired;
    private float _startTime;

    public ElapsedTimeStateTransition(State targetState, float duration, float elapsedTimeRequired)
        : base(targetState, duration)
    {
        _elapsedTimeRequired = elapsedTimeRequired;
    }

    public override void SetUp()
    {
        _startTime = Time.time;
    }

    public override bool IsSatisfied()
    {
        return Time.time - _startTime >= _elapsedTimeRequired;
    }
}
