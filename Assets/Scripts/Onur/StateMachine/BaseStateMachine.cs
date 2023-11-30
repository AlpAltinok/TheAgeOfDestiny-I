using System;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    BaseState currentState;

    private void Start()
    {
        currentState = GetInitalState();
        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    private void LateUpdate()
    {
        if(currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitalState()
    {
        return null;
    }

    public void AnimationEvent(string animationName)
    {
        currentState.AnimationEvent(animationName);
    }
}
