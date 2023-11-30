using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerAttackState : BaseScourgerState
{

    ScourgerAttackState_FlySmash flySmashState;
    ScourgerAttackState_SlashBack slashBackState;
    
    public ScourgerAttackState(ScourgerStateMachine stateMachine) : base("Attack", stateMachine)
    {

        flySmashState = new ScourgerAttackState_FlySmash(stateMachine);
        slashBackState = new ScourgerAttackState_SlashBack(stateMachine);
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.isAtacking = true;
        bossStateMachine.monsterScript.wasLongAttack = false;

        if (bossStateMachine.monsterScript.inRange)
        {
            Transform targetTransform = bossStateMachine.monsterScript.playerTarget;
            Transform bossTransform = bossStateMachine.monsterScript.gameObject.transform;

            Vector3 directionToCheck = targetTransform.position - bossTransform.position;

            Vector3 forwardDirection = bossTransform.forward;

            float dotProduct = Vector3.Dot(directionToCheck.normalized, forwardDirection);
            float behindThreshold = -0.5f;

            if (dotProduct < behindThreshold)
            {
                // The objectToCheck is considered behind the referenceObject.
                Debug.Log("Behind");
                
                bossStateMachine.ChangeState(slashBackState);

            }
            else
            {
                // The objectToCheck is not behind the referenceObject.
                Debug.Log("Not Behind");
                bossStateMachine.ChangeState(flySmashState);
                

            }
        }
        else
        {
            bossStateMachine.ChangeState(bossStateMachine.idleState);
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
    }

    public override void AnimationEvent(string animationName)
    {
        base.AnimationEvent(animationName);
            
    }

    public override void Exit()
    {
        base.Exit();
    }

}
