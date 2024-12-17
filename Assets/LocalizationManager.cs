using UniRx;

public interface ILocalizationManager
{
    IReadOnlyReactiveProperty<LocalizationLanguage> CurrentLanguage { get; }
    int CurrentLanguageAsInt { get; }
    string GetLocalizedText(string key);

    void SetLanguage(LocalizationLanguage language);
    void SetLanguage(int language);
}

public class LocalizationManager : ILocalizationManager
{
    private readonly LocalizationData _localizationData;
    private ReactiveProperty<LocalizationLanguage> _currentLanguage = new();
    public IReadOnlyReactiveProperty<LocalizationLanguage> CurrentLanguage => _currentLanguage;
    public int CurrentLanguageAsInt => (int)_currentLanguage.Value;

    public LocalizationManager(LocalizationData localizationData)
    {
        _localizationData = localizationData;
    }

    public string GetLocalizedText(string key)
    {
        return _localizationData.GetTextByID(key, _currentLanguage.Value);
    }

    public void SetLanguage(LocalizationLanguage language)
    {
        _currentLanguage.Value = language;
    }

    public void SetLanguage(int language)
    {
        _currentLanguage.Value = (LocalizationLanguage)language;
    }
}
