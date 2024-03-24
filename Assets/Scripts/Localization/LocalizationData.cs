using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/Localization Data")]
public class LocalizationData : ScriptableObject
{
    public LocalizationKey[] localizationKeys;

    public string GetTextByID(string id, LocalizationLanguage language)
    {
        foreach (LocalizationKey key in localizationKeys)
        {
            if (key.ID == id)
            {
                return key.GetLocalizedText(language);
            }
        }
        return "Text not found";
    }
}
