using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GoogleSheetsParser
{
    // Метод для парсинга данных из Google Sheets по ссылке
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