using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    PlayerData playerData;

    void Start()
    {
        playerData = SaveSystem.LoadPlayerData();
    }

    public void PlayGame()
    {
        if (playerData.sceneIndex < 5)
        {
            SceneManager.LoadScene("TutorialLevel(" + playerData.sceneIndex + ")");
        }
        else
        {
            SceneManager.LoadScene(1);
        }
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

    public void QuitGame()
    {
        try
        {
            SaveContent();
        }
        catch (Exception)
        {

        }
        Debug.Log("QUIT");
        Application.Quit();
    }

    private void SaveContent()
    {
        int y = SceneManager.GetActiveScene().buildIndex;

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        LevelData levelData = new LevelData(enemies, items);
        SaveSystem.SaveLevelData(SceneManager.GetActiveScene().name, levelData);

        PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>(), true, y);
        SaveSystem.SavePlayerData(playerData);
    }
}
