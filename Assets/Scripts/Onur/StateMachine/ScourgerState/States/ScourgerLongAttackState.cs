using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerLongAttackState : BaseScourgerState
{

    ScourgerLongAttackStates_ThrowSlash throwSlash;
    public ScourgerLongAttackState(ScourgerStateMachine stateMachine) : base("Boss Long Attack", stateMachine)
    {
        throwSlash = new ScourgerLongAttackStates_ThrowSlash(bossStateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.isAtacking = true;
        bossStateMachine.monsterScript.wasLongAttack = true;
        if (bossStateMachine.monsterScript.isAtacking)
        {
            bossStateMachine.ChangeState(throwSlash); 
        }
        else
        {
            bossStateMachine.ChangeState(bossStateMachine.moveState);
        }
    }
}
