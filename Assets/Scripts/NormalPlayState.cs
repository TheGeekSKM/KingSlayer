using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayState : CombatState
{
    //add info here if you have to
    [SerializeField] GameObject _normalMenus;
    [SerializeField] GameObject _combatMenus;

    public override void Enter()
    {
        Cursor.visible = false;
        Debug.Log("We are now in play mode");
        //StateMachine.Input.PressedConfirm += OnPressedConfirm;
        _combatMenus.SetActive(false);
        _normalMenus.SetActive(true);
    }

    public override void Exit()
    {
        Cursor.visible = true;
        _normalMenus.SetActive(false);
        Debug.Log("we are now exiting play mode");
        //StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        StateMachine.ChangeState<EnterCombatState>();
    }
}
