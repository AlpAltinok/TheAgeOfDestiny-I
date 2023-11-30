using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerMoveState : BaseScourgerState
{
    public ScourgerMoveState(ScourgerStateMachine stateMachine) : base("Move", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (bossStateMachine.monsterScript.inRange)
        {
            bossStateMachine.ChangeState(bossStateMachine.attackState); 
        }
        //TODO: if will change
        else if (bossStateMachine.monsterScript.targetDistance > ScourgerBossScript.throwDistance && !bossStateMachine.monsterScript.wasLongAttack)
        {
            bossStateMachine.ChangeState(bossStateMachine.longAttackState);
        }
        else
        {
            bossStateMachine.monsterScript.UpdatePath();
        }
    }
}
