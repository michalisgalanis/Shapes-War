using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavingSystem {
    private static readonly string FILE_SUFFIX = "/progress.stpd";

    private static readonly string WINDOWS_PATH = Application.dataPath + FILE_SUFFIX;
    private static readonly string ANDROID_PATH = "/storage/emulated/0/Armpra" + FILE_SUFFIX;

    private static string path;
    public static void SaveProgress(PlayerExperience playerExperience, Shield shield, GameObject gameManager) {
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        Data data = new Data(playerExperience, shield, gameManager, gameManager.GetComponent<StoreSystem>());
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data LoadData() {
        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("FileNotFound");
            return null;
        }
    }

    public static void SetPath() {
        switch (Application.platform) {
            case RuntimePlatform.WindowsEditor:
            path = WINDOWS_PATH;
            break;
            case RuntimePlatform.Android:
            path = ANDROID_PATH;
            break;
        }
    }
}
