using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    PlayerData playerData;
    PlayerData randomPlayerData;

    void Start()
    {
        playerData = SaveSystem.LoadPlayerData();
        randomPlayerData = SaveSystem.LoadPlayerData("Random");
    }

    public void ContinueRandomGame()
    {
        if (File.Exists(Application.persistentDataPath + "/RandomPlayer.lvl"))
        {
            SceneManager.LoadScene("RandomScene1");
        }
        
    }

    public void PlayTutorial()
    {
        for(int i = 1; i < 6; ++i)
        {
            try
            {
                File.Delete("TutorialLevel(" + i + ")");
            }
            catch (Exception)
            {

            }
        }
        SceneManager.LoadScene("TutorialLevel(1)");
    }

    public void PlayRandomGen()
    {
        SceneManager.LoadScene("RandomGeneratedLevelStart");
    }

    public void BackToMain()
    {
        SaveContent();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame(string gameType = "")
    {
        try
        {
            SaveContent(gameType);
        }
        catch (Exception)
        {

        }
        Debug.Log("QUIT");
        Application.Quit();
    }

    private void SaveContent(string gameType = "")
    {
        int y = SceneManager.GetActiveScene().buildIndex;

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        LevelData levelData = new LevelData(enemies, items);
        SaveSystem.SaveLevelData(SceneManager.GetActiveScene().name, levelData);
        PlayerData player;
        if (gameType == "Random")
        {
            player = new PlayerData(GameObject.FindObjectOfType<Player>(), true, GameObject.FindObjectOfType<Player>().currentLevel);
            SaveSystem.SavePlayerData(player, gameType);
        }
        else
        {
            player = new PlayerData(GameObject.FindObjectOfType<Player>(), true, y);
            SaveSystem.SavePlayerData(player, gameType);
        }
        
    }
}
