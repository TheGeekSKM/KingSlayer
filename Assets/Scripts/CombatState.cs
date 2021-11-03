using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatStateMachine))]
public class CombatState : State
{
    protected CombatStateMachine StateMachine {get; private set;}

    void Awake()
    {
        StateMachine = GetComponent<CombatStateMachine>();
    }
}
