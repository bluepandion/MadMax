using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState state;

    public void SetState(IState s)
    {
        if (state != null)
        {
            state.Exit();
        }
        state = s;
        state.Enter();
    }

    public void Update()
    {
        if (state != null)
        {
            state.Update();
        }
    }
}

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}