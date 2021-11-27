using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] int _healthAmount;
    [SerializeField] int _maxHealthAmount = 10;
    [SerializeField] int _lives;
    // [SerializeField] TextMeshPro _healthText;

    [SerializeField] Slider _healthSlider;


    public event Action DeathEvent = delegate { };

    public int HealthAmount 
    {
        get 
        {
            return _healthAmount;
        }
        set
        {
            _healthAmount = value;
        }
    }

    public int Lives 
    {
        get 
        {
            return _lives;
        }
        set
        {
            _lives = value;
        }
    }

    private void Start()
    {
        _healthAmount =  _maxHealthAmount;
        if (_healthSlider != null)
        {
            _healthSlider.maxValue = _maxHealthAmount;
        }
    }
    private void Update()
    {
        if (_healthAmount <= 0)
        {
            Death();
        }

        if (_healthSlider != null)
        {
            _healthSlider.value = _healthAmount;
        }

        // _healthText.text = "HP: " + _healthAmount.ToString();
    }


    public void DecreaseHealth(int damage)
    {
        _healthAmount -= damage;
    }

    public void IncreaseHealth(int healing)
    {
        if (_healthAmount < _maxHealthAmount)
        {
            _healthAmount += healing;
        }
    }

    void Death()
    {
        DeathEvent?.Invoke();
        gameObject.SetActive(false);
    }

}
