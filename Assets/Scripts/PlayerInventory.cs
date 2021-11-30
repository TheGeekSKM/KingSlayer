using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    //variables
    public event Action GotAllKeys = delegate { };
    [SerializeField] int _numKeysCollected;
    [SerializeField] int _numKeysNeeded;
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
