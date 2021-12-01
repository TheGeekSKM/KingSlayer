using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotionCollectible : CollectibleBase
{
    [SerializeField] float _speedIncrease = 10f;
    [SerializeField] float _speedTime = 3f;

    protected override void Collect(PlayerInventory player)
    {
        player._playerMovement.StaminaBoost();
    }

    //IEnumerator IncreaseSpeed(PlayerInventory _player, float secondsOfSpeed)
    //{
    //    _player._playerMovement.movementSpeed += _speedIncrease;
    //    yield return new WaitForSeconds(secondsOfSpeed);
    //    _player._playerMovement.movementSpeed -= _speedIncrease;
    //}
}
