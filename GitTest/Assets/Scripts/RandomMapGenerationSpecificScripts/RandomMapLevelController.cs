using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMapLevelController : MonoBehaviour
{
    List<String> itemNames;
    List<float[]> itemPositions;
    Enemy[] enemies;
    List<float[]> enemyPositions;
    List<bool> enemyState;
    GameObject[] _doors;
    LevelData levelData;
    PlayerData playerData;
    Player player;

    void Start()
    {
        Debug.Log("Started");
        player = GameObject.FindObjectOfType<Player>();
        _doors = GameObject.FindGameObjectsWithTag("Door");
        playerData = SaveSystem.LoadPlayerData();

        if (playerData != null)
        {
            player.currentLevel = playerData.sceneIndex;
            player.SetCurrentHealth(playerData.health);
            if (playerData.isInfected)
            {
                player.Infect();
            }

            foreach (var itemName in playerData.itemNames)
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


        levelData = SaveSystem.LoadLevelData(player.currentLevel.ToString());
        

        if (levelData != null)
        {
            itemNames = new List<string>();
            foreach (var item in levelData.itemNames)
            {
                itemNames.Add(item);

            }

            itemPositions = new List<float[]>();
            foreach (var pos in levelData.itemPositions)
            {
                if (pos[0] == 1000f)
                {
                    float[] newPos = new float[2] { UnityEngine.Random.Range(-6f, 7f), UnityEngine.Random.Range(-3f, 2.5f) };
                    itemPositions.Add(newPos);
                }
                else
                {
                    itemPositions.Add(pos);
                }
            }

            for (int i = 0; i < itemNames.Count; ++i)
            {
                GameObject pref = ItemPrefHandler.instance.FindPrefByName(itemNames[i]);
                Instantiate(pref, new Vector2(itemPositions[i][0], itemPositions[i][1]), transform.rotation);
            }

            enemyPositions = new List<float[]>();
            foreach (var pos in levelData.enemyPositions)
            {
                if (pos[0] == 1000f)
                {
                    float[] newPos = new float[2] { UnityEngine.Random.Range(-6f, 7f), UnityEngine.Random.Range(-3f, 2.5f) };
                    enemyPositions.Add(newPos);
                }
                else
                {
                    enemyPositions.Add(pos);
                }
            }

            enemyState = new List<bool>();

            for(int i = 0; i < levelData.isEnabled.Length; ++i)
            {
                enemyState.Add(levelData.isEnabled[i]);
            }

            for (int i = 0; i < levelData.enemyNames.Length; ++i)
            {
                GameObject pref = ItemPrefHandler.instance.FindEnemyPrefByName(levelData.enemyNames[i].Replace("(Clone)","").Trim());
                Instantiate(pref, new Vector2(enemyPositions[i][0], enemyPositions[i][1]), transform.rotation);
            }

        }

        enemies = GameObject.FindObjectsOfType<Enemy>();
        if(enemyState != null)
        {
            for(int i = 0; i < enemyState.Count; ++i)
            {
                if (!enemyState[i])
                {
                    enemies[i].SetMaxHealth(-100f);
                }
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
        foreach (var door in _doors)
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
                    return false;
                }
            }
        }
        return true;
    }
}
