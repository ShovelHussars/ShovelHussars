using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_500 : Item
{
    
    public override void Use()
    {
        player = GameObject.FindObjectOfType<Player>();
        player.Heal(50f);
        Inventory.instance.Consume(this);
    }
}
