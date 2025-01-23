
public class WalkState : BaseState
{
    public WalkState(Character character) : base(character) {}

    public override void OnStateEnter()
    {
        character.SetAnimation(EState.Walk);
    }

    public override void OnStateUpdate()
    {
        if(!GameManager.Instance.IsGaming)
        {
            character.StateMachine.ChangeState(EState.Idle);
            return;
        }

        if(character.FindEnemyOnDistance())
            character.StateMachine.ChangeState(EState.Attack);

        character.Move();

        OnUpdate?.Invoke();
    }

    public override void OnStateExit()
    {
        
    }
}
