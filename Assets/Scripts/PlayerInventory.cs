using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    //variables
    public event Action GotAllKeys = delegate { };
    [SerializeField] int _numKeysCollected;
    [SerializeField] int _numKeysNeeded;
    [SerializeField] TextMeshProUGUI _keyText;
    [SerializeField] public Health playerHealth;
    public int NumKeysCollected 
        {
        get
        {
            return _numKeysCollected;
        }
        
        set
        {
            _numKeysCollected = value;
        }
    }

    void Update()
    {
        if (_numKeysCollected >= _numKeysNeeded)
        {
            GotAllKeys?.Invoke();
        }

        _keyText.text = _numKeysCollected.ToString();

        Debug.Log(_numKeysCollected);
    }



    public void AddKey()
    {
        _numKeysCollected++;
    }

    public void AddKey(int keyNums)
    {
        _numKeysCollected += keyNums;
    }
}
