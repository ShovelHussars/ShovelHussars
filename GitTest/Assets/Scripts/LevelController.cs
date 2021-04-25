using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    List<ItemPickup> items;
    List<String> itemNames;
    List<float[]> itemPositions;
    Enemy[] enemies;
    GameObject[] _doors;
    LevelData levelData;
    PlayerData playerData;
    Player player;

    void Start()
    {
        Debug.Log("Started");
        player = GameObject.FindObjectOfType<Player>();
        enemies = GameObject.FindObjectsOfType<Enemy>();
        _doors = GameObject.FindGameObjectsWithTag("Door");
        playerData = SaveSystem.LoadPlayerData();
        levelData = SaveSystem.LoadLevelData(SceneManager.GetActiveScene().name);
        ItemPickup[] temp = GameObject.FindObjectsOfType<ItemPickup>();
        items = new List<ItemPickup>();

        if (playerData != null)
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

            if (!playerData.leftOnRightSide)
            {
                Vector2 newSpanwLocation = GameObject.FindGameObjectWithTag("RightDoor").transform.position;
                newSpanwLocation.x -= 0.1f;
                player.transform.position = newSpanwLocation;

            }
        }

        if(levelData != null)
        {
            foreach (var item in temp)
            {
                items.Add(item);
                
            }
            itemNames = new List<string>();
            foreach (var item in levelData.itemNames)
            {
                itemNames.Add(item);
                
            }
            itemPositions = new List<float[]>();
            foreach (var pos in levelData.itemPositions)
            {
                itemPositions.Add(pos);
            }

            for (int i = 0; i < levelData.enemyNames.Length; ++i)
            {
                for(int j = 0; j < enemies.Length; j++)
                {
                    if (enemies[j].name == levelData.enemyNames[i])
                    {
                        if (!levelData.isEnabled[i])
                        {
                            enemies[j].TakeDamage(200f);
                        }
                        Vector2 newPosition = new Vector2(levelData.enemyPositions[i][0], levelData.enemyPositions[i][1]);
                        enemies[j].transform.position = newPosition;
                        enemies[j].transform.rotation = Quaternion.Euler(0F, levelData.enemyRotation[i], 0F);
                    }
                }
            }

            for (int i = 0; i < itemNames.Count; ++i)
            {
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j].item.name == itemNames[i])
                    {
                        Debug.Log(items[j].item.name);
                        Vector2 newPosition = new Vector2(itemPositions[i][0], itemPositions[i][1]);
                        items[j].transform.position = newPosition;
                        items.RemoveAt(j);
                        itemNames.RemoveAt(i);
                        itemPositions.RemoveAt(i);
                        break;
                    }
                }
            }
            for(int i = 0; i < itemNames.Count; ++i)
            {
                GameObject pref = ItemPrefHandler.instance.FindPrefByName(itemNames[i]);
                Instantiate(pref, new Vector2(itemPositions[i][0], itemPositions[i][1]), transform.rotation);
            }
            foreach(var item in items)
            {
                item.Destroy();
            }
        }
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
