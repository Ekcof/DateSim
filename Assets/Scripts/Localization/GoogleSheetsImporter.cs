using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "GoogleSheetImporter", menuName = "Localization/Google Sheet Importer")]
public class GoogleSheetImporter : ScriptableObject
{
    public LocalizationData localizationData; // ������ �� ScriptableObject ��� �������� ������
    private const string googleSheetURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vT_oEn4-xwbckixNwhMbVOtmHrknWBoGKkdhZmiLdA0sdrGnRkM6vEwsfBQAVB83YomMbgYUrIyO8bA/pub?output=csv";

    public void ImportData()
    {
        if (localizationData != null)
        {
            localizationData.localizationKeys = GoogleSheetImporterEditor.ParseGoogleSheet(googleSheetURL);
            EditorUtility.SetDirty(localizationData); // �������� ������ ��� ����������, ����� Unity �������� ���
        }
    }
}

[CustomEditor(typeof(LocalizationData))]
public class GoogleSheetImporterEditor : Editor
{
    [MenuItem("Tools/Import Data from Google Sheet")]
    private static void ImportDataFromGoogleSheet()
    {
        // ����� �� ������ �������� ������ �� ��� GoogleSheetImporter ���������
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