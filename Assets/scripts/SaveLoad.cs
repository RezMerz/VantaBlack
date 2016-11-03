using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad  {
    public static void SaveScene(int scene)
    {
        string name = (string)scene.ToString();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath+"/Saves/" + name + ".ktk", FileMode.OpenOrCreate);
        
        file.Close();
    }

    public static void SaveMap(MapMenu map)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Saves/map.ktk", FileMode.OpenOrCreate);
        bf.Serialize(file,map);
        file.Close();
    }

    public static MapMenu LoadMap()
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
}
