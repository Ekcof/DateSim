using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

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

    // ����� ��� �������� ������ �� ������
    private static List<LocalizationKey> ParseData(string data)
    {
        List<LocalizationKey> localizationList = new List<LocalizationKey>();

        // ���������� ������ �� ������
        string[] lines = data.Split('\n');

        // �������� �� ������ ������, ������� �� ������ (������ ������ �������� ���������)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            // ���������� ������ �� ������ �� �����������
            string[] cells = line.Split(',');

            if (cells.Length >= 4) // ��������, ��� ������ �������� ����������� ������
            {
                // �������� ������� LocalizationKey � ���������� ��� ������� �� Google Sheets
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