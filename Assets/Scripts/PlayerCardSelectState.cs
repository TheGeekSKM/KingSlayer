using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCardSelectState : CombatState
{
    [SerializeField] TextMeshProUGUI _playerTurnTextUI;
    [SerializeField] GameObject _playerCardPanel;
    [SerializeField] float _runPercentage;

    //Player and Enemy Variables
    [SerializeField] Health _playerHealth;
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

    bool _attackCard = false;
    bool _healCard = false;
    bool _runCard = false;

    int _playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Turn:....Entering");
        _playerTurnTextUI.gameObject.SetActive(true);
        _playerCardPanel.SetActive(true);

        _playerTurnCount++;
        _playerTurnTextUI.text = "Your Turn";

        //hook into events
        StateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    public override void Exit()
    {
        // _playerTurnTextUI.gameObject.SetActive(false);
        _playerTurnTextUI.text = "Enemy's Turn";
        _playerCardPanel.SetActive(false);

        Debug.Log("Player Turn:....Exiting");

        //unhook from events
        StateMachine.Input.PressedConfirm -= OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        if (_attackCard)
        {
            AttackEnemy();
        }
        else if (_healCard)
        {
            HealPlayers();
        }
        else if (_runCard)
        {
            RunAway();
        }
        Debug.Log("Attempt to enter Enemy State");
        // _playerTurnTextUI.text = "Enemy's Turn";
        StateMachine.ChangeState<EnemyTurnCombatState>();
    }

    void AttackEnemy()
    {
        Debug.Log("Damaged the Enemy");
        _enemyHealth.DecreaseHealth(2);
    }

    void HealPlayers()
    {
        _playerHealth.IncreaseHealth(2);
    }

    void RunAway()
    {
        // _playerHealth.DecreaseHealth(100);
        float randomPercentage = Random.Range(1f, 100f);
        Debug.Log(randomPercentage);

        if (randomPercentage < _runPercentage)
        {

            // StateMachine.ChangeState<NormalPlayState>();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }


    #region Select Methods
    public void AttackCardSelect()
    {
        _attackCard = true;
        _healCard = false;
        _runCard = false;
    }

    public void HealCardSelect()
    {
        _attackCard = false;
        _healCard = true;
        _runCard = false;
    }

    public void RunCardSelect()
    {
        _attackCard = false;
        _healCard = false;
        _runCard = true;
    }
    #endregion
   
}
