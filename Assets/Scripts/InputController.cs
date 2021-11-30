using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action PressedConfirm = delegate { };
    public event Action PressedActionConfirm = delegate { };
    public event Action PressedCancel = delegate { };
    public event Action PressedLeft = delegate { };
    public event Action PressedRight = delegate { };

    // Update is called once per frame
    void Update()
    {  
        DetectConfirm();
        DetectCancel();
        DetectLeft();
        DetectRight();
        DetectAttackConfirm();
        
    }

    private void DetectRight()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PressedRight?.Invoke();
        }
    }
    private void DetectLeft()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PressedLeft?.Invoke();
        }
    }
    private void DetectCancel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PressedCancel?.Invoke();
        }
    }
    public void DetectConfirm()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    PressedConfirm?.Invoke();
        //}
    }

    public void DetectAttackConfirm()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    PressedConfirm?.Invoke();
        //}
    }
}
