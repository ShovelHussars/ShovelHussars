using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] bool nextLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CollidedWitPlayer(collision))
        {
            GoToNextLevel();
        }
    }

    private bool CollidedWitPlayer(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null && player.tag.Equals("Player"))
        {
            return true;
        }
        return false;
    }

    void GoToNextLevel()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        PlayerData playerData = new PlayerData(GameObject.FindObjectOfType<Player>());
        SaveSystem.SavePlayerData(playerData);
        
        if (nextLevel)
        {
            if (SceneManager.sceneCountInBuildSettings - 1 == y)
            {
                string path = Application.persistentDataPath + "/Player.lvl";
                File.Delete(path);
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(y + 1);
            }
        }
        else
        {
            SceneManager.LoadScene(y - 1);
        }
    }
}
