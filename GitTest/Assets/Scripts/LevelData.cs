using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string sceneName;
    public float[] lastPlayerPosition;
    public string[] entityNames;
    public string[] itemNames;

    public LevelData(string _sceneName, Entity[] entities, ItemPickup[] items, Player player)
    {
        sceneName = _sceneName;
        lastPlayerPosition = new float[3];
        lastPlayerPosition[0] = player.transform.position.x;
        lastPlayerPosition[1] = player.transform.position.y;
        lastPlayerPosition[2] = player.transform.position.z;
        entityNames = new string[entities.Length];
        for(int i = 0; i < entities.Length; ++i)
        {
            entityNames[i] = entities[i].name;
        }
        itemNames = new string[items.Length];
        for (int i = 0; i < items.Length; ++i)
        {
            itemNames[i] = items[i].name;
        }
    }
}
