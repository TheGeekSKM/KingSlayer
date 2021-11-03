using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCardSelectState : CombatState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn:....Entering");
        _playerTurnTextUI.gameObject.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Player Turn: " + _playerTurnCount.ToString();

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        _playerTurnTextUI.gameObject.SetActive(false);

        Debug.Log("Player Turn:....Exiting");

        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        Debug.Log("Attempt to enter Enemy State");
        StateMachine.ChangeState<EnemyTurnCombatState>();
    }

   
}
