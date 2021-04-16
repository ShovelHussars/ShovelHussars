using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Guard[] _guards;
    GameObject[] _doors;
    // Start is called before the first frame update
    void Start()
    {
        _guards = GameObject.FindObjectsOfType<Guard>();
        _doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // Update is called once per frame
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
        if ((_guards != null) && (_guards.Length != 0))
        {
            foreach (var guard in _guards)
            {
                if (guard.enabled)
                {
                    //print("false");
                    return false;
                }
            }
        }
        return true;
    }
}
