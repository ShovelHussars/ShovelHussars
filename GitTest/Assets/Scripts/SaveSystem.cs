using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SaveLevelData(string sceneName, LevelData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/"+sceneName+".lvl";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static LevelData LoadLevelData(string sceneName)
    {
        string path = Application.persistentDataPath + "/" + sceneName + ".lvl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData levelData = formatter.Deserialize(stream) as LevelData;

            stream.Close();
            return levelData;
        }
        else
        {
            Debug.Log("No such save yet.");
            return null;
        }
    }

    public static void SavePlayerData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player.lvl";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/Player.lvl";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return playerData;
        }
        else
        {
            Debug.Log("No such save yet.");
            return null;
        }
    }

    public static void GenerateRandomMap(int length)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        for (int i = 0; i < length; ++i)
        {
            Debug.Log("Generating Random shit");
            string path = Application.persistentDataPath + "/" + i+".lvl";
            try
            {
                File.Delete(path + "/" + i + ".lvl");
            }
            catch (Exception)
            {

            }
            FileStream stream = new FileStream(path, FileMode.Create);
            LevelData data = RandomLevelMaker.instance.GenerateLevel();
            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
}
