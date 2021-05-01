using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string sceneName;
    public string[] enemyNames;
    public bool[] isEnabled;
    public float[][] enemyPositions;
    public float[] enemyRotation;
    public string[] itemNames;
    public float[][] itemPositions;

    public LevelData(string _sceneName, Enemy[] enemies, ItemPickup[] items)
    {
        sceneName = _sceneName;
        enemyNames = new string[enemies.Length];
        isEnabled = new bool[enemies.Length];
        enemyPositions = new float[enemies.Length][];
        enemyRotation = new float[enemies.Length];
        itemNames = new string[items.Length];
        itemPositions = new float[items.Length][];

        for(int i = 0; i < enemies.Length; ++i)
        {
            enemyNames[i] = enemies[i].name;
            isEnabled[i] = enemies[i].enabled;
            enemyPositions[i] = new float[2];
            enemyPositions[i][0] = enemies[i].transform.position.x;
            enemyPositions[i][1] = enemies[i].transform.position.y;
            enemyRotation[i] = enemies[i].transform.rotation.y;
        }
        
        for (int i = 0; i < items.Length; ++i)
        {
            itemNames[i] = items[i].item.name;
            itemPositions[i] = new float[2];
            itemPositions[i][0] = items[i].transform.position.x;
            itemPositions[i][1] = items[i].transform.position.y;
        }
    }
}
