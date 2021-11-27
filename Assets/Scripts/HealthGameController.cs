using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthGameController : MonoBehaviour
{
    [SerializeField] Health _playerHealth;


    void OnEnable()
    {
        _playerHealth.DeathEvent += OnPlayerDeath;

    }

    void OnDisable()
    {
        _playerHealth.DeathEvent -= OnPlayerDeath;

    }

    void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

   


    
}
