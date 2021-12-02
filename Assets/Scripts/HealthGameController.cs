using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthGameController : MonoBehaviour
{
    [SerializeField] Health _playerHealth;
    [SerializeField] PlayerInventory _playerInventory;


    void OnEnable()
    {
        _playerHealth.DeathEvent += OnPlayerDeath;
        _playerInventory.GotAllKeys += OnKeysGotten;

    }

    void OnDisable()
    {
        _playerHealth.DeathEvent -= OnPlayerDeath;
        _playerInventory.GotAllKeys -= OnKeysGotten;

    }

    void OnPlayerDeath()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    void OnKeysGotten()
    {
        Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   


    
}
