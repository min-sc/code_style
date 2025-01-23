using System;

public class AttackState : BaseState
{
    public AttackState(Character character, Action onEnter = null, Action onUpdate = null, Action onExit = null) : base(character) 
    {
        base.OnEnter = onEnter;
        base.OnUpdate = onUpdate;
        base.OnExit = onExit;
    }

    public override void OnStateEnter()
    {
        character.SetAnimation(EState.Attack);
    }

    public override void OnStateUpdate()
    {
        if(!GameManager.Instance.IsGaming)
        {
            character.StateMachine.ChangeState(EState.Idle);
            return;
        }
        
        if(!character.FindEnemyOnDistance())
        {
            character.StateMachine.ChangeState(EState.Walk);
            return;
        }

        OnUpdate?.Invoke();
    }

    public override void OnStateExit()
    {
        
    }
}
