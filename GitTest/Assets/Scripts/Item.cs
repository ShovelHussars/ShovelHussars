using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    protected Player player;

    public virtual void Use()
    {
        if(name == "SCP-012")
        {
            player = GameObject.FindObjectOfType<Player>();
            player.TakeDamage(100f);
        }
    }
}
