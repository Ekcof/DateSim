using Spectrum.Settings;
using UnityEngine;


[CreateAssetMenu(fileName = "SerializedDataHolder", menuName = "Game Resources/SerializedDataHolder")]
public class SerializedDataHolder : ScriptableObject
{
    SerializedStats _statsData;
    SerializedPlayerSettings _playerSettings;
    SerializedCoolDowns _coolDowns;
    SerializedNPCData _npcData;
    SerializedMainStatsData _mainStatsData;

    public SerializedStats StatsData => _statsData;
    public SerializedPlayerSettings PlayerSettings => _playerSettings;
    public SerializedCoolDowns CoolDowns => _coolDowns;
    public SerializedNPCData NPCData => _npcData;
    public SerializedMainStatsData MainStatsData => _mainStatsData;
    public bool HasSave() => _statsData != null;

    public Language CurrentLanguage => (Language)_playerSettings.Language;

    public void LoadSerializedData()
    {
        // TODO: Move to block with dependence on platform for saving
        var statsData = PlayerPrefsUtils.LoadObject<SerializedStats>("statsData");
        var settings = PlayerPrefsUtils.LoadObject<SerializedPlayerSettings>("PlayerSettings");
        var coolDowns = PlayerPrefsUtils.LoadObject<SerializedCoolDowns>("CoolDowns");
        var npcData = PlayerPrefsUtils.LoadObject<SerializedNPCData>("NPCData");
        var mainStatsData = PlayerPrefsUtils.LoadObject<SerializedMainStatsData>("MainStatsData");

        _statsData = statsData ?? new SerializedStats();
        _playerSettings = settings ?? new SerializedPlayerSettings();
        _coolDowns = coolDowns ?? new SerializedCoolDowns();
        _npcData = npcData ?? new SerializedNPCData();
        _mainStatsData = mainStatsData ?? new SerializedMainStatsData();
    }

    public void SaveSerializedData(SerializedStats statsData, SerializedPlayerSettings settings,
        SerializedCoolDowns coolDowns, SerializedNPCData npcData,
        SerializedMainStatsData mainStats)
    {
        // TODO: Move to block with dependence on platform for saving
        _statsData = statsData;
        _playerSettings = settings;
        _coolDowns = coolDowns;
        _npcData = npcData;
        _mainStatsData = mainStats;

        PlayerPrefsUtils.SaveObject("statsData", statsData);
        PlayerPrefsUtils.SaveObject("PlayerSettings", settings);
        PlayerPrefsUtils.SaveObject("CoolDowns", coolDowns);
        PlayerPrefsUtils.SaveObject("NPCData", npcData);
        PlayerPrefsUtils.SaveObject("MainStatsData", mainStats);
    }
}
