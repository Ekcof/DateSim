using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class GoogleSheetsParser
{
    // ����� ��� �������� ������ �� Google Sheets �� ������
    public static LocalizationKey[] ParseGoogleSheet(string googleSheetURL)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        UnityWebRequest www = UnityWebRequest.Get(googleSheetURL);
        www.SendWebRequest();

        while (!www.isDone) { }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("������ �������� ������ �� Google Sheets: " + www.error);
        }
        else
        {
            // �������� ����� �� Google Sheets
            string data = www.downloadHandler.text;
            // ������ ������
            localizationList = ParseData(data);
        }

        return localizationList.ToArray();
    }

    private static List<LocalizationKey> ParseData(string data)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        // ��������� ����� �� ������
        string[] lines = data.Split('\n');

        // �������� �� ������ ������, ������� �� ������ (������ ������ �������� ���������)
        for (int i = 1; i < lines.Length; i++)
        {
            // ��������� ������, �������� �������
            List<string> cells = SplitCsvLine(lines[i]);

            if (cells.Count >= 4) // ���������, ��� � ��� ���� ��� ����������� ������
            {
                // ������� ������ LocalizationKey � ��������� ��� �������
                LocalizationKey key = new LocalizationKey();
                key.ID = cells[0];
                key.Text_EN = cells[2];
                key.Text_RU = cells[3];
                localizationList.Add(key);
            }
        }

        return localizationList;
    }

    // ����� ��� ���������� CSV ������ � ������ �������
    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        var inQuotes = false;
        var value = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '\"') // ������������� � ����� �������
            {
                Debug.Log("Found quote");
                inQuotes = !inQuotes;
            }
            else
            if (c == ',' && !inQuotes) // ����� ��������, ���� �� � ��������
            {
                result.Add(value.ToString());
                value.Length = 0; // ������� StringBuilder ��� ���������� ��������
            }
            else
            {
                value.Append(c);
            }
        }

        // ��������� ��������� �������� ����� ������ �� �����
        result.Add(value.ToString());

        // �������� ������� � ������ � ����� ��������, ���� ��� ����
        for (int i = 0; i < result.Count; i++)
        {
            result[i] = result[i].Trim(); // ������� �������
            if (result[i].StartsWith("\"") && result[i].EndsWith("\""))
            {
                result[i] = result[i].Substring(1, result[i].Length - 2);
            }
        }

        return result;
    }
}