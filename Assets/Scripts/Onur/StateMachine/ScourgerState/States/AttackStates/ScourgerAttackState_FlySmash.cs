using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScourgerAttackState_FlySmash : BaseScourgerState
{
    public ScourgerAttackState_FlySmash(ScourgerStateMachine stateMachine) : base("Attack_FlySmash", stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        bossStateMachine.monsterScript.animator.SetInteger("AnimationNumber", 0);
    }

    public override void AnimationEvent(string animationName)
    {
        base.AnimationEvent(animationName);
        if (animationName == "Animation Ended")
            bossStateMachine.ChangeState(bossStateMachine.attackState);
    }
}
