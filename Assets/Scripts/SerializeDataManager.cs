using System;
//using Spectrum.Localization;
using DateSim.Settings;
using UnityEngine;
using Zenject;

public class SerializedData
{
    //public SerializedLevelManager LevelManager;
    //public SerializedStats Stats;
}

public class SerializeDataManager : MonoBehaviour
{
    // [Inject] SerializedDataHolder _saveHolder;
    // [Inject] LevelManager _levelManager;
    //[Inject] StatsManager _stats;
    [Inject] ILocalizationManager _localizationManager;
    [Inject] ISettingsManager _settingsManager;
    [Inject] ICoolDownManager _coolDownManager;
    [Inject] INPCProgressManager _npcManager;
    [Inject] IMainStatsManager _mainStatsManager;

    private void Awake()
    {
        Deserialize();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void SaveData()
    {
        SerializedPlayerSettings settings = _settingsManager.Serialize();
        SerializedCoolDowns coolDowns = _coolDownManager.Serialize();
        SerializedNPCData npcData = _npcManager.Serialize();
        SerializedMainStatsData statsData = _mainStatsManager.Serialize();
        //SerializedLevelManager levelData = _levelManager.Serialize();
        //SerializedStats statsData = _stats.Serialize();
        //SerializedPlayerSettings settings = new SerializedPlayerSettings()
        //{
        //    Language = _localizationManager.CurrentLanguageAsInt,
        //    DeviceName = SystemInfo.deviceName,
        //    DeviceModel = SystemInfo.deviceModel,
        //    GraphicsPreset = (int)_settingsManager.GraphicsLevel.Value
        //};
        //SerializedCoolDowns coolDowns = _coolDownManager.Serialize();

        //_saveHolder.SaveSerializedData(levelData, statsData, settings, coolDowns);
    }

    public void Deserialize(Action onDone = null)
    {
        //var levelData = _saveHolder.LevelsData;
        //var statsData = _saveHolder.StatsData;
        //var settings = _saveHolder.PlayerSettings;
        //var coolDowns = _saveHolder.CoolDowns;

        //if (levelData == null)
        //{
        //    levelData = new();
        //}
        //if (statsData == null)
        //{
        //    statsData = new();
        //}
        //if (settings == null)
        //{
        //    settings = new();
        //}
        //if (coolDowns == null)
        //{
        //    coolDowns = new();
        //}

        //_levelManager.Deserialize(levelData);
        //_stats.Deserialize(statsData);

        //onDone?.Invoke();
    }
}
