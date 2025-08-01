using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string savePath => Application.persistentDataPath + "/save.json";

    public static void SaveGame()
    {
        SaveData data = new SaveData
        {
            gold = Inventory.gold,
            wood = Inventory.resourceCounts[0],
            metal = Inventory.resourceCounts[1],

            itemCounts = Inventory.itemCounts,
            itemPrices = Inventory.itemPrices,

            speedUpgradeLevels = UpgradeUI.speedUpgradeLevels,
            secondUpgradeLevels = UpgradeUI.secondUpgradeLevels,

        };

        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(savePath, json);
        Debug.Log("Гру збережено: " + savePath);
    }

    public static void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Файл збереження не знайдено.");
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Inventory.gold = data.gold;
        Inventory.resourceCounts[0] = data.wood;
        Inventory.resourceCounts[1] = data.metal;

        Inventory.itemCounts = data.itemCounts;
        Inventory.itemPrices = data.itemPrices;

        UpgradeUI.speedUpgradeLevels = data.speedUpgradeLevels;
        UpgradeUI.secondUpgradeLevels = data.secondUpgradeLevels;
    }

    public static bool SaveExists()
    {
        return File.Exists(savePath);
    }
}