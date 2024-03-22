using System.IO;
using UnityEngine;


public class SerializationManager
{
    private readonly string _filePath = Path.Combine(Application.dataPath, "gameData.json");

    // Структура для хранения данных игры
    [System.Serializable]
    private struct GameData
    {
        public int Health;
        public int Level;
        public int Money;
        public NPCProgress[] NPCs; // Добавляем массив прогрессов NPC
    }

    // Метод для сериализации данных в JSON
    public void SaveGameData(int health, int level, int money, NPCProgress[] npcs)
    {
        GameData data = new GameData
        {
            Health = health,
            Level = level,
            Money = money,
            NPCs = npcs
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(_filePath, json);
    }

    // Метод для десериализации данных из JSON
    public void LoadGameData(out int health, out int level, out int money, out NPCProgress[] npcs)
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

            health = data.Health;
            level = data.Level;
            money = data.Money;
            npcs = data.NPCs;
        }
        else
        {
            health = 100; // Значение по умолчанию
            level = 1; // Значение по умолчанию
            money = 0; // Значение по умолчанию
            npcs = new NPCProgress[0]; // Пустой массив NPC как значение по умолчанию
        }
    }
}