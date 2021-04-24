using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float health;
    public bool isInfected;
    public string[] itemNames;

    public PlayerData(Player player)
    {
        health = player.GetCurrentHealth();
        isInfected = player.GetIsInfected();
        itemNames = new string[Inventory.instance.items.Count];
        Item[] temp = Inventory.instance.items.ToArray();
        for (int i = 0; i < itemNames.Length; ++i)
        {
            itemNames[i] = temp[i].name;
        }
    }
}
