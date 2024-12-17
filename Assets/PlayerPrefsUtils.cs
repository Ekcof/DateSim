using UnityEngine;

public static class PlayerPrefsUtils
{
    public static void SaveObject(string key, object obj)
    {
        string json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(key, json);
    }

    public static T LoadObject<T>(string key)
    {
        string json = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(json);
    }
}