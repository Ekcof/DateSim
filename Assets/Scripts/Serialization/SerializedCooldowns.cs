using System;
using DateSim.Rewards;
using UnityEngine;

[Serializable]
public class CoolDownsArray
{
    public RewardCoolDownData[] CoolDowns;
}

[Serializable]
public class SerializedCoolDowns
{
    public string CoolDownsJson;

    public RewardCoolDownData[] CoolDowns
    {
        get
        {
            if (string.IsNullOrEmpty(CoolDownsJson))
            {
                return new RewardCoolDownData[0];
            }

            try
            {
                Debug.Log($"Deserializing CoolDownsJson: {CoolDownsJson}");
                var coolDownArray = JsonUtility.FromJson<CoolDownsArray>(CoolDownsJson);
                return coolDownArray != null && coolDownArray.CoolDowns != null ? coolDownArray.CoolDowns : new RewardCoolDownData[0];
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deserializing CoolDownsJson: {e.Message}");
                Debug.LogError(e.StackTrace);
                return new RewardCoolDownData[0];
            }
        }
        set
        {
            try
            {
                CoolDownsJson = JsonUtility.ToJson(new CoolDownsArray { CoolDowns = value });
                Debug.Log($"Serialized CoolDownsJson: {CoolDownsJson}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error serializing CoolDowns: {e.Message}");
                Debug.LogError(e.StackTrace);
            }
        }
    }

    public SerializedCoolDowns() { }
}