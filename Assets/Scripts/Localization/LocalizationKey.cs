public enum LocalizationLanguage
{
    English,
    Russian
}

// ����� ��� �������� ������ �� Google Sheets
[System.Serializable]
public class LocalizationKey
{
    public string ID;
    public string Text_EN;
    public string Text_RU;

    public string GetLocalizedText(LocalizationLanguage language)
    {
        return language switch
        {
            LocalizationLanguage.English => Text_EN,
            LocalizationLanguage.Russian => Text_RU,
            _ => Text_EN,
        };
    }
}

