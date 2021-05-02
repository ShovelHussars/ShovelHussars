using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationStarter : MonoBehaviour
{
    
    void Start()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        
        Debug.Log("WorldGenerationStart");
        Debug.Log(LengthOfRandomRun.instance.lengthOfRandomRun);
        SaveSystem.GenerateRandomMap(LengthOfRandomRun.instance.lengthOfRandomRun);
    }
}
