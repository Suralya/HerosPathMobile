using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{

    public static Game SavedGame = new Game();

    public static void Save()
    {
        SavedGame = Game.currentGame;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.HeroPath");
        bf.Serialize(file, SaveLoad.SavedGame);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.HeroPath"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.HeroPath", FileMode.Open);
            SaveLoad.SavedGame = (Game)bf.Deserialize(file);
            Game.currentGame = SaveLoad.SavedGame;
            file.Close();
        }
        else
        {
            SaveLoad.SavedGame = null;
            Debug.Log("no file found");
        }

    }
}
