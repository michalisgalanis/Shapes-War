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
                pathRoot = Utility.Data.WINDOWS_PATH_ROOT;
                break;
            case RuntimePlatform.WindowsEditor:
                pathRoot = Utility.Data.EDITOR_PATH_ROOT;
                break;
            case RuntimePlatform.Android:
                pathRoot = Utility.Data.ANDROID_PATH_ROOT;
                break;
        }
    }

    public void Save(Utility.Data.dataTypes dataType) {
        FileStream stream = null;
        BinaryFormatter formatter = new BinaryFormatter();
        switch (dataType) {
            case Utility.Data.dataTypes.PLAYER_MAP_DATA:
                stream = new FileStream(pathRoot + Utility.Data.PLAYER_MAP_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.PlayerMapData());
                break;
            case Utility.Data.dataTypes.STORE_DATA:
                stream = new FileStream(pathRoot + Utility.Data.STORE_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.StoreData());
                break;
            case Utility.Data.dataTypes.SETTINGS_DATA:
                stream = new FileStream(pathRoot + Utility.Data.SETTINGS_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.SettingsData());
                break;
            case Utility.Data.dataTypes.STATS_DATA:
                stream = new FileStream(pathRoot + Utility.Data.STATS_DATA, FileMode.OpenOrCreate);
                formatter.Serialize(stream, new Data.StatsData());
                break;
        }
        if (stream != null)
            stream.Close();
    }
    public void SaveAll() {
        for (int i = 0; i < Enum.GetValues(typeof(Utility.Data.dataTypes)).Length; i++) {
            Save((Utility.Data.dataTypes) Enum.GetValues(typeof(Utility.Data.dataTypes)).GetValue(i));
        }
    }

    public void Load(Utility.Data.dataTypes dataType) {
            FileStream stream = null;
        BinaryFormatter formatter = new BinaryFormatter();
        switch (dataType) {
            case Utility.Data.dataTypes.PLAYER_MAP_DATA:
                if (!File.Exists(pathRoot + Utility.Data.PLAYER_MAP_DATA)) {
                    Data.PlayerMapData.Reset();
                    break;
                }
                stream = new FileStream(pathRoot + Utility.Data.PLAYER_MAP_DATA, FileMode.OpenOrCreate);
                Data.PlayerMapData playerMapData = (formatter.Deserialize(stream) as Data.PlayerMapData);
                playerMapData.Load();
                break;
            case Utility.Data.dataTypes.STORE_DATA:
                if (!File.Exists(pathRoot + Utility.Data.STORE_DATA))
                    break;
                stream = new FileStream(pathRoot + Utility.Data.STORE_DATA, FileMode.OpenOrCreate);
                Data.StoreData storeData = (formatter.Deserialize(stream) as Data.StoreData);
                storeData.Load();
                break;
            case Utility.Data.dataTypes.SETTINGS_DATA:
                if (!File.Exists(pathRoot + Utility.Data.SETTINGS_DATA))
                    break;
                stream = new FileStream(pathRoot + Utility.Data.SETTINGS_DATA, FileMode.OpenOrCreate);
                Data.SettingsData settingsData = (formatter.Deserialize(stream) as Data.SettingsData);
                settingsData.Load();
                break;
            case Utility.Data.dataTypes.STATS_DATA:
                if (!File.Exists(pathRoot + Utility.Data.STATS_DATA))
                    break;
                stream = new FileStream(pathRoot + Utility.Data.STATS_DATA, FileMode.OpenOrCreate);
                Data.StatsData statsData = (formatter.Deserialize(stream) as Data.StatsData);
                statsData.Load();
                break;
        }
        if (stream != null) stream.Close();
    }
    public void LoadAll() {
        for (int i = 0; i < Enum.GetValues(typeof(Utility.Data.dataTypes)).Length; i++) {
            Load((Utility.Data.dataTypes)Enum.GetValues(typeof(Utility.Data.dataTypes)).GetValue(i));
        }
    }

    public void DeleteProgress(Utility.Data.dataTypes dataType) {
        switch (dataType) {
            case Utility.Data.dataTypes.PLAYER_MAP_DATA:
                File.Delete(pathRoot + Utility.Data.PLAYER_MAP_DATA);
                File.Delete(pathRoot + Utility.Data.PLAYER_MAP_DATA + ".meta");
                Data.PlayerMapData.Reset();
                break;
            case Utility.Data.dataTypes.STORE_DATA:
                File.Delete(pathRoot + Utility.Data.STORE_DATA);
                File.Delete(pathRoot + Utility.Data.STORE_DATA + ".meta");
                Data.StoreData.Reset();
                break;
            case Utility.Data.dataTypes.SETTINGS_DATA:
                File.Delete(pathRoot + Utility.Data.SETTINGS_DATA);
                File.Delete(pathRoot + Utility.Data.SETTINGS_DATA + ".meta");
                Data.SettingsData.Reset();
                break;
            case Utility.Data.dataTypes.STATS_DATA:
                File.Delete(pathRoot + Utility.Data.STATS_DATA);
                File.Delete(pathRoot + Utility.Data.STATS_DATA + ".meta");
                Data.StatsData.Reset();
                break;
        }
    }

    public static SavingSystem getInstance() {
        return instance ?? new SavingSystem();
    }
}