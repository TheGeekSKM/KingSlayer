using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCombatState : CombatState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] int numEnemyHeals = 3;

    [SerializeField] Health _enemyHealth;
    [SerializeField] Health _playerHealth;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Turn:...Enter");
        EnemyTurnBegan?.Invoke();

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
    }

    IEnumerator EnemyThinkingRoutine(float pauseDuration)
    {
        Debug.Log("Enemy Thinking....");
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Enemy performs action");

        if (_enemyHealth.HealthAmount <= 4 && numEnemyHeals > 0)
        {
            _enemyHealth.IncreaseHealth(2);
            numEnemyHeals--;
        }
        else
        {
            _playerHealth.DecreaseHealth(2);
        }

        EnemyTurnEnded?.Invoke();

        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
