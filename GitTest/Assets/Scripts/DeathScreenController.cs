using System.Collections;
using System.Collections.Generic;
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
}
