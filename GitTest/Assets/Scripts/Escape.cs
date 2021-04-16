using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject escMenu;
    public static bool GameIsPaused = false;
    

    private void Start()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }


        }
        
    }

    public void Resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        escMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

}
