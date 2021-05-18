using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefHandler : MonoBehaviour
{
    #region Singleton
    public static ItemPrefHandler instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public List<GameObject> prefabs = new List<GameObject>();
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    public GameObject FindPref(Item item)
    {
        foreach(var prefab in prefabs)
        {
            if(prefab.GetComponent<ItemPickup>().item.name == item.name)
            {
                return prefab;
            }
        }
        return null;
    }

    public GameObject FindPrefByName(string name)
    {
        foreach (var prefab in prefabs)
        {
            if (prefab.GetComponent<ItemPickup>().item.name == name)
            {
                return prefab;
            }
        }
        return null;
    }

    public GameObject FindEnemyPref(Enemy enemy)
    {
        foreach (var prefab in enemyPrefabs)
        {
            if (prefab.GetComponent<Enemy>().name == enemy.name)
            {
                return prefab;
            }
        }
        return null;
    }

    public GameObject FindEnemyPrefByName(string name)
    {
        foreach (var prefab in enemyPrefabs)
        {
            if (prefab.GetComponent<Enemy>().name == name)
            {
                return prefab;
            }
        }
        return null;
    }
}
