using UnityEngine;

public class BaseState
{
	public string name;
    protected BaseStateMachine baseStateMachine;

	public BaseState(string name, BaseStateMachine stateMachine)
	{
		this.name = name;
		baseStateMachine = stateMachine;
	}

	public virtual void Enter() 
	{
		Debug.Log("Entered " + name);
	}
	public virtual void UpdateLogic() { }
    public virtual void UpdatePhysics() { }

	public virtual void AnimationEvent(string animationName) { }
    public virtual void Exit() { }
}
