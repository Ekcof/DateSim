using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "GoogleSheetImporter", menuName = "Localization/Google Sheet Importer")]
public class GoogleSheetImporter : ScriptableObject
{
    public LocalizationData localizationData; // ссылка на ScriptableObject для хранения данных
    private const string googleSheetURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vT_oEn4-xwbckixNwhMbVOtmHrknWBoGKkdhZmiLdA0sdrGnRkM6vEwsfBQAVB83YomMbgYUrIyO8bA/pub?output=csv";

    public void ImportData()
    {
        if (localizationData != null)
        {
            localizationData.localizationKeys = GoogleSheetImporterEditor.ParseGoogleSheet(googleSheetURL);
            EditorUtility.SetDirty(localizationData); // Помечаем объект как измененный, чтобы Unity сохранял его
        }
    }
}

[CustomEditor(typeof(LocalizationData))]
public class GoogleSheetImporterEditor : Editor
{
    [MenuItem("Tools/Import Data from Google Sheet")]
    private static void ImportDataFromGoogleSheet()
    {
        // Здесь вы должны получить ссылку на ваш GoogleSheetImporter экземпляр
        GoogleSheetImporter importer = FindObjectOfType<GoogleSheetImporter>();
        if (importer != null)
        {
            importer.ImportData();
        }
        else
        {
            Debug.LogError("GoogleSheetImporter instance not found in the scene!");
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GoogleSheetImporter importer = (GoogleSheetImporter)target;

        if (GUILayout.Button("Import Data from Google Sheet"))
        {
            importer.ImportData();
        }
    }

    public static LocalizationKey[] ParseGoogleSheet(string googleSheetURL)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        UnityWebRequest www = UnityWebRequest.Get(googleSheetURL);
        www.SendWebRequest();

        while (!www.isDone) { }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка загрузки данных из Google Sheets: " + www.error);
        }
        else
        {
            // Получаем текст из Google Sheets
            string data = www.downloadHandler.text;
            // Парсим данные
            localizationList = ParseData(data);
        }

        return localizationList.ToArray();
    }

    // Метод для парсинга данных из текста
    private static List<LocalizationKey> ParseData(string data)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        // Разделение текста на строки
        string[] lines = data.Split('\n');

        // Проходим по каждой строке, начиная со второй (первая строка содержит заголовки)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            // Разделение строки на ячейки по разделителю
            string[] cells = line.Split(',');

            if (cells.Length >= 4) // Проверка, что строка содержит необходимые данные
            {
                // Создание объекта LocalizationKey и заполнение его данными из Google Sheets
                LocalizationKey key = new LocalizationKey();
                key.ID = cells[0];
                key.Text_EN = cells[2];
                key.Text_RU = cells[3];
                localizationList.Add(key);
            }
        }

        return localizationList;
    }
}