using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionCollectible : CollectibleBase
{
    //[SerializeField] int _healingAmount = 2;

    //public int HealingAmount { get; set; }

    protected override void Collect(PlayerInventory player)
    {
        player.AddHealPotion();
    }

    
}
