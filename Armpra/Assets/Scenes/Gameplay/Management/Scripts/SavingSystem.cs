using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{    
    public static void SaveProgress(PlayerStats player, GameplayManager gManager,ShieldPowerUp shield,SpeedPowerUp rushB)
    {
        string path = Application.dataPath + "/progress.stoopid";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        Data data = new Data(player, gManager, shield, rushB);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadData()
    {
        string path = Application.dataPath + "/progress.stoopid";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        }else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
