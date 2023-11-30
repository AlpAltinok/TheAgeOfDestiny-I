using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerAttackState_SlashBack : BaseScourgerState
{
    public ScourgerAttackState_SlashBack(ScourgerStateMachine stateMachine) : base("Attack_SlashBack", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.animator.SetInteger("AnimationNumber", 1);
    }

    public override void AnimationEvent(string animationName)
    {
        base.AnimationEvent(animationName);
        if (animationName == "Animation Ended")
        {
            bossStateMachine.monsterScript.gameObject.transform.Rotate(Vector3.up, 180);
            bossStateMachine.ChangeState(bossStateMachine.attackState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
