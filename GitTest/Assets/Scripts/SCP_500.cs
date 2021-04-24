using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCP_500 : Item
{
    Player player;

    public override void Use()
    {
        player = GameObject.FindObjectOfType<Player>();
        Debug.Log(player);
        player.Heal(50f);
        Inventory.instance.Consume(this);
    }
}
