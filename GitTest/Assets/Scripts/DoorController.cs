using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] string _nextLevel;

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
        SceneManager.LoadScene(_nextLevel);
    }
}
