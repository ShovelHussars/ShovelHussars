using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelMaker: MonoBehaviour
{
    #region Singleton
    public static RandomLevelMaker instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    readonly List<Enemy> enemies = new List<Enemy>();
    readonly List<ItemPickup> items = new List<ItemPickup>();

    public LevelData GenerateLevel()
    {
        for(int i = 0; i < ItemPrefHandler.instance.enemyPrefabs.Count; ++i)
        {
            int random = Random.Range(0,3);
            for(int j = 0; j < random; ++j)
            {
                enemies.Add(ItemPrefHandler.instance.enemyPrefabs[i].GetComponent<Enemy>());
                enemies[j].transform.position = new Vector2(1000f, 1000f);
            }
        }

        for (int i = 0; i < ItemPrefHandler.instance.prefabs.Count; ++i)
        {
            int random = Random.Range(0, 2);
            for (int j = 0; j < random; ++j)
            {
                items.Add(ItemPrefHandler.instance.prefabs[i].GetComponent<ItemPickup>());
                items[j].transform.position = new Vector2(1000f, 1000f);
            }
        }

        LevelData levelData = new LevelData(enemies.ToArray(), items.ToArray());
        enemies.Clear();
        items.Clear();
        return levelData;
    }

}
