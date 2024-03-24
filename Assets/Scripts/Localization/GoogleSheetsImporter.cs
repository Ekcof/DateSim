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
            localizationData.localizationKeys = GoogleSheetsParser.ParseGoogleSheet(googleSheetURL);
            EditorUtility.SetDirty(localizationData); // Помечаем объект как измененный, чтобы Unity сохранял его
        }
    }
}

[CustomEditor(typeof(GoogleSheetImporter))]
public class GoogleSheetImporterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GoogleSheetImporter importer = (GoogleSheetImporter)target;

        if (GUILayout.Button("Import Data from Google Sheet"))
        {
            importer.ImportData();
        }
    }
}