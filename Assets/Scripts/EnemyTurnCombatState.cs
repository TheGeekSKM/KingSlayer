using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCombatState : CombatState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] int numEnemyHeals = 3;
    [SerializeField] AudioClip _playerHurtSound;

    [SerializeField] Health _enemyHealth;
    public Health EnemyHealth 
    {
        get
        {
            return _enemyHealth;
        }
        set
        {
            _enemyHealth = value;
        }
    }
    [SerializeField] Health _playerHealth;

    [SerializeField] float _pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Turn:...Enter");
        EnemyTurnBegan?.Invoke();
        _enemyHealth.DeathEvent += OnEnemyDeath;

        StartCoroutine(EnemyThinkingRoutine(_pauseDuration));
    }

   

    public override void Exit()
    {
        Debug.Log("Enemy Turn: Exit...");
        _enemyHealth.DeathEvent -= OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        StateMachine.ChangeState<NormalPlayState>();
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
            if (_playerHurtSound != null)
            {
                AudioHelper.PlayClip2D(_playerHurtSound, 1f);
            }
        }

        EnemyTurnEnded?.Invoke();

        StateMachine.ChangeState<PlayerCardSelectState>();
    }
}
