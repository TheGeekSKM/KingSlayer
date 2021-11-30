using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : CollectibleBase
{
   
    protected override void Collect(PlayerInventory player)
    {
        player.AddKey();
        Destroy(gameObject);
    }


}
