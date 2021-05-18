using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void DeleteCurrentRandomScenes()
    {
        for (int i = 0; i < LengthOfRandomRun.instance.lengthOfRandomRun; ++i)
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/" + i + ".lvl");

            }
            catch (Exception)
            {

            }
        }
        File.Delete(Application.persistentDataPath + "/RandomPlayer.lvl");
    }
}
