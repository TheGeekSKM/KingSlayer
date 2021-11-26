using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("StateMachine Connections")]
    [SerializeField] CombatStateMachine _stateMachine;
    [SerializeField] PlayerCardSelectState _pCSState;
    [SerializeField] EnemyTurnCombatState _eTCState;
    [SerializeField] EnterCombatState _eCState;

    [Header("Own Connections")]
    [SerializeField] Health _enemyHealth;
    [SerializeField] Transform _enemyTransform;

    void Awake()
    {
        _pCSState.EnemyHealth = _enemyHealth;
        _eTCState.EnemyHealth = _enemyHealth;
        _eCState.EnemyTransform = this.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        _stateMachine.ChangeState<EnemyTurnCombatState>();
    }
}
