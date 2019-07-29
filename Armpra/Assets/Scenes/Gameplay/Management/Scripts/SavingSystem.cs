using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem{    
    private static string path =  Application.dataPath + "/progress.stpd";
    public static void SaveProgress(PlayerStats playerStatsComponent, Shield shield, SpeedPowerUp speedPowerUp, GameObject gameManager)
    {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            Data data = new Data(playerStatsComponent,shield,speedPowerUp,gameManager);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            stream.Close();
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
