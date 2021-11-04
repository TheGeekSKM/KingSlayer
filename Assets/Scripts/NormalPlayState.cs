using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayState : CombatState
{
    //add info here if you have to
    public override void Enter()
    {
        Debug.Log("We are now in play mode");
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        Debug.Log("we are now exiting play mode");
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnterCombatState>();
    }
}
