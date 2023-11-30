using UnityEngine;

public class ScourgerIdleState : BaseScourgerState
{
    public ScourgerIdleState(ScourgerStateMachine stateMachine) : base("Idle", stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.isAtacking = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (!bossStateMachine.monsterScript.inRange)
        {
            //TODO: if will change
            if (bossStateMachine.monsterScript.targetDistance > ScourgerBossScript.throwDistance && !bossStateMachine.monsterScript.wasLongAttack)
            {
                bossStateMachine.ChangeState(bossStateMachine.longAttackState);
            }
            else
            {
                bossStateMachine.ChangeState(bossStateMachine.moveState); 
            }
        }
        else
        {
            bossStateMachine.monsterScript.LookAtTarget();
        }
        
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit() 
    { 
        base.Exit();
    }
}

