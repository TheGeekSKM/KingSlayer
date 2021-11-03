using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    #region  Variables and Properties
    public State CurrentState => _currentState;
    protected bool InTransition { get; private set;}

    State _currentState;
    protected State _previousState;
    #endregion


    public void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.LogWarning("CANNOT CHANGE STATE. STATE DOESN'T EXIST. PLEASE ATTACH DESIRED STATE");
            return;
        }
        InitiateStateChange(targetState);
    }

    public void RevertState()
    {
        if (_previousState != null)
        {
            InitiateStateChange(_previousState);
        }
    }

    void InitiateStateChange(State targetState)
    {
        if (_currentState != targetState && !InTransition)
        {
            Transition(targetState);
        }
    }

    void Transition(State newState)
    {
        //start transition
        InTransition = true;

        //transitioning
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
        
        //end transition
        InTransition = false;
    }

    private void Update()
    {
        if (CurrentState != null && !InTransition)
        {
            CurrentState.Tick();
        }
    }

    
}
