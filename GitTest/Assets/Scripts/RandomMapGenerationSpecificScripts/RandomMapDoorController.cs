using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMapDoorController : MonoBehaviour
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
        if (player != null && player.CompareTag("Player"))
        {
            return true;
        }
        return false;
    }

    void GoToNextLevel()
    {
        int y = player.currentLevel;

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        LevelData levelData = new LevelData(enemies, items);
        SaveSystem.SaveLevelData(y.ToString(), levelData);

        try
        {
            if (nextLevel)
            {
                if (y == LengthOfRandomRun.instance.lengthOfRandomRun - 1)
                {
                    string path = Application.persistentDataPath;
                    File.Delete(path + "/RandomPlayer.lvl");
                    for (int i = 0; i < LengthOfRandomRun.instance.lengthOfRandomRun; i++)
                    {
                        File.Delete(path + "/" + i + ".lvl");
                    }
                    SceneManager.LoadScene(0);
                }
                else
                {
                    PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>(), true, ++y);
                    SaveSystem.SavePlayerData(playerData, "Random");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            else
            {
                PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>(), false, --y);
                SaveSystem.SavePlayerData(playerData, "Random");
                Debug.Log("About to load prev Scene");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        catch (Exception)
        {

        }
    }
}
