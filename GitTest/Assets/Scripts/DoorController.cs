using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] bool nextLevel;
    Player player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollidedWitPlayer(collision))
        {
            GoToNextLevel();
        }
    }

    private bool CollidedWitPlayer(Collision2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if(player != null && player.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    void GoToNextLevel()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        LevelData levelData = new LevelData(enemies, items);
        SaveSystem.SaveLevelData(SceneManager.GetActiveScene().name, levelData);


        if (nextLevel)
        {
            if (SceneManager.sceneCountInBuildSettings - 3 == y)
            {
                string path = Application.persistentDataPath;
                File.Delete(path + "/Player.lvl");
                for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
                {
                    File.Delete(path + "/TutorialLevel(" + i + ").lvl");
                }
                SceneManager.LoadScene(0);
            }
            else
            {
                PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>(), true, 0);
                SaveSystem.SavePlayerData(playerData);
                SceneManager.LoadScene(y + 1);
            }
        }
        else
        {
            PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>(), false, 0);
            SaveSystem.SavePlayerData(playerData);
            Debug.Log("About to load prev Scene");
            SceneManager.LoadScene(y - 1);
        }
    }
}
