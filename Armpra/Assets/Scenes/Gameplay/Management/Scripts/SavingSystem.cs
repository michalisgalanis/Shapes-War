using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavingSystem {
    private static string path = Application.dataPath + "/progress.stpd";

    public static void SaveProgress() {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        Data data = new Data();
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadData() {
        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        }
        return null;
    }

    public static void SetPath() {
        switch (Application.platform) {
            case RuntimePlatform.WindowsPlayer:
            path = Constants.FileLocations.WINDOWS_PATH;
            break;
            case RuntimePlatform.WindowsEditor:
            path = Constants.FileLocations.WINDOWS_PATH;
            break;
            case RuntimePlatform.Android:
            path = Constants.FileLocations.ANDROID_PATH;
            break;
        }
    }
}
