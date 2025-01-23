using System;

public abstract class BaseState
{
    protected Character character;

    public BaseState(Character character)
    {
        this.character = character;
    }

    public Action OnEnter;
    public Action OnUpdate;
    public Action OnExit;

    public abstract void OnStateEnter();
    public abstract void OnStateUpdate();
    public abstract void OnStateExit();
}