using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCombatState : CombatState
{
    [SerializeField] int _startingCardNumber;
    [SerializeField] int _numOfPlayers = 2;
    [SerializeField] Transform _enemyTransform;
    public Transform EnemyTransform
    {
        get
        {
            return _enemyTransform;
        }
        set
        {
            _enemyTransform = value;
        }
    }
    [SerializeField] Transform _playerTransform;

    // [SerializeField] Camera _mainCamera;
    // [SerializeField] Vector3 _combatCameraPosition;
    // [SerializeField] Transform _playerTransform;
    // [SerializeField] GameObject _virtualCamera;
 
    bool _activated = false;

    public override void Enter()
    {
        Debug.Log("Setup:....Entering");
        Debug.Log("Creating " + _numOfPlayers + " players");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");
        // _mainCamera.transform.position = _playerTransform.position + _combatCameraPosition;
        // _mainCamera.transform.rotation = Quaternion.identity;
        // _virtualCamera.gameObject.SetActive(false);

        _enemyTransform.position = new Vector3(_playerTransform.position.x - 3, _enemyTransform.position.y, _playerTransform.position.z - 3);        

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
