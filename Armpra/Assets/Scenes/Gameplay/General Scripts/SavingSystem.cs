using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    
    public static void SaveProgress(PlayerStats player, GameplayManager gManager)
    {
        string path = Application.persistentDataPath + "/progress.stoopid";
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(player, gManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data loadData()
    {
        string path = Application.persistentDataPath + "/progress.stoopid";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            stream.Close();

            Data data = formatter.Deserialize(stream) as Data;
            return data;
        }else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
