using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthGameController : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] Health _enemyHealth;

    void OnEnable()
    {
        _playerHealth.DeathEvent += OnPlayerDeath;
        _enemyHealth.DeathEvent += OnEnemyDeath;
    }

    void OnDisable()
    {
        _playerHealth.DeathEvent -= OnPlayerDeath;
        _enemyHealth.DeathEvent -= OnEnemyDeath;
    }

    void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    void OnEnemyDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    
}
