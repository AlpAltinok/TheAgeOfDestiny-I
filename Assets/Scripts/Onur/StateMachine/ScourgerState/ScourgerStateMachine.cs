using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerStateMachine : BaseStateMachine
{
    //States   
    public ScourgerBossScript monsterScript;
    [HideInInspector]
    public ScourgerIdleState idleState;
    [HideInInspector]
    public ScourgerMoveState moveState;
    [HideInInspector]
    public ScourgerAttackState attackState;
    [HideInInspector]
    public ScourgerLongAttackState longAttackState;


    private void Awake()
    {
        idleState = new ScourgerIdleState(this);
        moveState = new ScourgerMoveState(this);
        attackState = new ScourgerAttackState(this);
        longAttackState = new ScourgerLongAttackState(this);
    }

    protected override BaseState GetInitalState()
    {
        return idleState;
    }
}
