using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

public class SavingSystem {
    private static SavingSystem instance;
    private static string pathRoot = "";

    private SavingSystem() {
        instance = this; //Prevents further instantiations
        switch (Application.platform) {
            case RuntimePlatform.WindowsPlayer:
                pathRoot = Constants.Data.WINDOWS_PATH_ROOT;
                break;
            case RuntimePlatform.WindowsEditor:
                pathRoot = Constants.Data.WINDOWS_PATH_ROOT;
                break;
            case RuntimePlatform.Android:
                pathRoot = Constants.Data.ANDROID_PATH_ROOT;
                break;
        }
    }

    public void Save(Constants.Data.dataTypes dataType) {
        FileStream stream = null;
        BinaryFormatter formatter = new BinaryFormatter();
        switch (dataType) {
            case Constants.Data.dataTypes.PLAYER_MAP_DATA:
                stream = new FileStream(pathRoot + Constants.Data.PLAYER_MAP_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.PlayerMapData());
                break;
            case Constants.Data.dataTypes.STORE_DATA:
                stream = new FileStream(pathRoot + Constants.Data.STORE_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.StoreData());
                break;
            case Constants.Data.dataTypes.AUDIO_DATA:
                stream = new FileStream(pathRoot + Constants.Data.AUDIO_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.AudioData());
                break;
        }
        if (stream != null)
            stream.Close();
    }
    public void SaveAll() {
        for (int i = 0; i < Enum.GetValues(typeof(Constants.Data.dataTypes)).Length; i++) {
            Save((Constants.Data.dataTypes) Enum.GetValues(typeof(Constants.Data.dataTypes)).GetValue(i));
        }
    }

    public void Load(Constants.Data.dataTypes dataType) {
            FileStream stream = null;
        BinaryFormatter formatter = new BinaryFormatter();
        switch (dataType) {
            case Constants.Data.dataTypes.PLAYER_MAP_DATA:
                if (!File.Exists(pathRoot + Constants.Data.PLAYER_MAP_DATA))
                    break;
                stream = new FileStream(pathRoot + Constants.Data.PLAYER_MAP_DATA, FileMode.OpenOrCreate);
                Data.PlayerMapData playerMapData = (formatter.Deserialize(stream) as Data.PlayerMapData);
                playerMapData.Load();
                break;
            case Constants.Data.dataTypes.STORE_DATA:
                if (!File.Exists(pathRoot + Constants.Data.STORE_DATA))
                    break;
                stream = new FileStream(pathRoot + Constants.Data.STORE_DATA, FileMode.OpenOrCreate);
                Data.StoreData storeData = (formatter.Deserialize(stream) as Data.StoreData);
                storeData.Load();
                break;
            case Constants.Data.dataTypes.AUDIO_DATA:
                if (!File.Exists(pathRoot + Constants.Data.AUDIO_DATA))
                    break;
                stream = new FileStream(pathRoot + Constants.Data.AUDIO_DATA, FileMode.OpenOrCreate);
                Data.AudioData audioData = (formatter.Deserialize(stream) as Data.AudioData);
                audioData.Load();
                break;
        }
        if (stream != null) stream.Close();
    }
    public void LoadAll() {
        for (int i = 0; i < Enum.GetValues(typeof(Constants.Data.dataTypes)).Length; i++) {
            Load((Constants.Data.dataTypes)Enum.GetValues(typeof(Constants.Data.dataTypes)).GetValue(i));
        }
    }

    public static SavingSystem getInstance() {
        return instance ?? new SavingSystem();
    }
}