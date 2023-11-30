using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerLongAttackStates_ThrowSlash : BaseScourgerState
{
    public ScourgerLongAttackStates_ThrowSlash(ScourgerStateMachine stateMachine) : base("Boss Long Attack Throw Slash", stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.ThrowSlash();
        bossStateMachine.ChangeState(bossStateMachine.idleState);
    }
}
