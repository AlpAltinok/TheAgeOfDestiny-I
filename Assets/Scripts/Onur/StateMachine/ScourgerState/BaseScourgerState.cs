using UnityEngine;

public class BaseScourgerState : BaseState
{
    protected ScourgerStateMachine bossStateMachine;
    
    public BaseScourgerState(string name, ScourgerStateMachine stateMachine) : base(name, stateMachine)
    {
        bossStateMachine = stateMachine;
    }
}
