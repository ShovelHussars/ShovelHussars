using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    ItemPickup[] items;
    Enemy[] enemies;
    GameObject[] _doors;
    LevelData levelData;
    PlayerData playerData;
    Player player;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        items = GameObject.FindObjectsOfType<ItemPickup>();
        enemies = GameObject.FindObjectsOfType<Enemy>();
        _doors = GameObject.FindGameObjectsWithTag("Door");
        //levelData = SaveSystem.LoadLevelData(SceneManager.GetActiveScene().name);
        playerData = SaveSystem.LoadPlayerData();

        if(playerData != null)
        {
            player.SetCurrentHealth(playerData.health);
            if (playerData.isInfected)
            {
                player.Infect();
            }
            
            foreach(var itemName in playerData.itemNames)
            {
                GameObject pref = ItemPrefHandler.instance.FindPrefByName(itemName);
                pref.GetComponent<ItemPickup>().Add();
            }
        }

        /*if(levelData != null)
        {
            player.transform.position = new Vector3(
                levelData.lastPlayerPosition[0],
                levelData.lastPlayerPosition[1],
                levelData.lastPlayerPosition[2]);
            for(int i = 0; i < levelData.entityNames.Length; ++i)
            {
                for(int j = 0; j < enemies.Length; j++)
                {
                    if (true)
                    {

                    }
                }
            }
        }*/
    }

    void Update()
    {
        if (EnemiesAreAllDead())
        {
            OpenAllDoors();
        }
    }

    private void OpenAllDoors()
    {
        foreach(var door in _doors)
        {
            GameObject.Destroy(door);
        }
    }

    private bool EnemiesAreAllDead()
    {
        if ((enemies != null) && (enemies.Length != 0))
        {
            foreach (var enemy in enemies)
            {
                if (enemy.enabled)
                {
                    //print("false");
                    return false;
                }
            }
        }
        return true;
    }
}
