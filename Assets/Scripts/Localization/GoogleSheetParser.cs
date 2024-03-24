using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

    private static List<LocalizationKey> ParseData(string data)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        // Разделяем текст на строки
        string[] lines = data.Split('\n');

        // Проходим по каждой строке, начиная со второй (первая строка содержит заголовки)
        for (int i = 1; i < lines.Length; i++)
        {
            // Разделяем строки, учитывая кавычки
            List<string> cells = SplitCsvLine(lines[i]);

            if (cells.Count >= 4) // Проверяем, что у нас есть все необходимые данные
            {
                // Создаем объект LocalizationKey и заполняем его данными
                LocalizationKey key = new LocalizationKey();
                key.ID = cells[0];
                key.Text_EN = cells[2];
                key.Text_RU = cells[3];
                localizationList.Add(key);
            }
        }

        return localizationList;
    }

    // Метод для разделения CSV строки с учетом кавычек
    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        var inQuotes = false;
        var value = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '\"') // Переключаемся в режим кавычек
            {
                Debug.Log("Found quote");
                inQuotes = !inQuotes;
            }
            else
            if (c == ',' && !inQuotes) // Конец значения, если не в кавычках
            {
                result.Add(value.ToString());
                value.Length = 0; // Очищаем StringBuilder для следующего значения
            }
            else
            {
                value.Append(c);
            }
        }

        // Добавляем последнее значение после выхода из цикла
        result.Add(value.ToString());

        // Удаление кавычек в начале и конце значения, если они есть
        for (int i = 0; i < result.Count; i++)
        {
            result[i] = result[i].Trim(); // Удаляем пробелы
            if (result[i].StartsWith("\"") && result[i].EndsWith("\""))
            {
                result[i] = result[i].Substring(1, result[i].Length - 2);
            }
        }

        return result;
    }
}