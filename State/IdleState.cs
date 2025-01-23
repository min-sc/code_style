
public class IdleState : BaseState
{
    public IdleState(Character character) : base(character) {}

    public override void OnStateEnter()
    {
        character.SetAnimation(EState.Idle);
    }

    public override void OnStateUpdate()
    {
        if(!GameManager.Instance.IsGaming)
            return;

        if(character.FindEnemyOnDistance())
        {
            character.StateMachine.ChangeState(EState.Attack);
            return;
        }

        if (character is Ally)
        {
            var ally = (Ally)character;
            if (ally.InitPosFromMine == Direc.Left)
                character.StateMachine.ChangeState(EState.Walk);
            else if(ally.InitPosFromMine == Direc.Right)
                character.StateMachine.ChangeState(EState.Idle);
        }

        character.StateMachine.ChangeState(EState.Walk);
    }

    public override void OnStateExit()
    {
        
    }
}
