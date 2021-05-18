using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float health;
    public int sceneIndex;
    public int amountOfLevels;
    public bool isInfected;
    public string[] itemNames;
    public bool leftOnRightSide = true;

    public PlayerData(Player player, bool _leftOnRightSide, int _sceneIndex)
    {
        health = player.GetCurrentHealth();

        sceneIndex = _sceneIndex;
        isInfected = player.GetIsInfected();
        itemNames = new string[Inventory.instance.items.Count];
        Item[] temp = Inventory.instance.items.ToArray();
        for (int i = 0; i < itemNames.Length; ++i)
        {
            itemNames[i] = temp[i].name;
        }
        leftOnRightSide = _leftOnRightSide;
    }
}
