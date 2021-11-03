using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCombatState : CombatState
{
    [SerializeField] int _startingCardNumber;
    [SerializeField] int _numOfPlayers = 2;

    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup:....Entering");
        Debug.Log("Creating " + _numOfPlayers + " players");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");

        _activated = false;
    }

    public override void Tick()
    {
        if (_activated == false)
        {
            _activated = true;
            StateMachine.ChangeState<PlayerCardSelectState>();
        }
    }

    public override void Exit()
    {
        _activated = false;
        Debug.Log("Setup:....Exitng");
    }
    
}
