using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad  {
    public static void _SaveScene(List<Unit>[,] units,string scene)
    {
        string name = (string)scene.ToString();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath+"/Saves/" + name + ".ktk", FileMode.OpenOrCreate);
        
        file.Close();
    }

    public static void _SaveMap(MapMenu map)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Saves/map.ktk", FileMode.OpenOrCreate);
        bf.Serialize(file,map);
        file.Close();
    }

    public static MapMenu _LoadMap()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.dataPath + "/Saves/map.ktk"))
        {
            FileStream file = File.Open(Application.dataPath + "/Saves/map.ktk", FileMode.Open);
            MapMenu map = (MapMenu)bf.Deserialize(file);
            file.Close();
            return map;
        }
        else
        {
            Debug.Log("File Does Not Exists");
            return null;
        }
    }


    public static Player _LoadPlayer()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File_Open_Null("saves/Player.ktk");
        Player player = null;
        if (file != null)
        {
            player = (Player)bf.Deserialize(file);
            file.Close();
        }
       
        return player;
    }
    public static void _SavePlayer(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File_Create_Open("/saves/Player.ktk");
        bf.Serialize(file, player);
        file.Close();
    }
    private static FileStream File_Open_Null(string path)
    {
        if (File.Exists(Application.dataPath + path))
        {
            FileStream file = File.Open(Application.dataPath + path, FileMode.Open);
            return file;
        }
        else
            return null;
    }
    private static FileStream File_Create_Open(string path)
    {
        FileStream file = File.Open(Application.dataPath + path, FileMode.OpenOrCreate);
        return file;
    }

}
