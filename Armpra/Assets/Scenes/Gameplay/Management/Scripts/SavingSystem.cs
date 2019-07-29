using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem{    
    private static string path =  Application.dataPath + "/progress.stpd";
    public static void SaveProgress(PlayerStats player, GameplayManager gManager,Shield shield,SpeedPowerUp rushB){
        if (!File.Exists(path)){
            FileStream stream = new FileStream(path, FileMode.Create);
            Data data = new Data(player, gManager, shield, rushB);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
        }   
    }

    public static Data LoadData(){
        if (File.Exists(path)){
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
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
