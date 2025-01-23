
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine
{
    public BaseState currentState;
    private List<BaseState> states;

    // public StateMachine(BaseState initState)
    // {
    //     currentState = initState;
    //     ChangeState(currentState);
    // }

    public void AddState(BaseState state)
    {
        if (states == null)
            states = new List<BaseState>();

        states.Add(state);
    }

    public void Initialize(BaseState initState)
    {
        currentState = initState;
        currentState.OnStateEnter();
    }

    public void Initialize(EState initState)
    {
        Initialize(GetStateByEnum(initState));
    }

    public void ChangeState(BaseState newState)
    {
        if (currentState == newState) 
            return;

        currentState?.OnStateExit();  
        currentState = newState;   
        currentState.OnStateEnter(); 
    }

    public void ChangeState(EState newState)
    {
        var state = GetStateByEnum(newState);

        if(state == null)
        {
            Debug.LogWarning("state is null");
            return;
        }
        
        ChangeState(state);
    }

    public BaseState GetStateByEnum(EState eState)
    {
        switch(eState)
        {
            case EState.Idle:   return states.Where((s) => s is IdleState).First();
            case EState.Walk:   return states.Where((s) => s is WalkState).First();
            case EState.Attack: return states.Where((s) => s is AttackState).First();
        }

        return null;
    }

    public void Update()
    {
        currentState?.OnStateUpdate();
    }
}
