using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string saveFolder = Application.persistentDataPath + "/saves/";
    private static readonly string saveExtension = ".leven";
    
    public static void SaveGame(int saveSlot)
    {
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        
        var formatter = new BinaryFormatter();
        var path = saveFolder + "save_" + saveSlot + saveExtension;
        var stream = new FileStream(path, FileMode.Create);
        
        var data = new SaveData();
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static SaveData LoadGame(int saveSlot)
    {
        var path = saveFolder + "save_" + saveSlot + saveExtension;
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);
            
            var data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            
            return data;
        }

        Debug.LogError("Save file not found in " + path);
        return null;
    }
    
    public static bool SaveExists(int saveSlot)
    {
        var path = saveFolder + "save_" + saveSlot + saveExtension;
        return File.Exists(path);
    }
}

[Serializable]
public class SaveData
{
    // Game State
    public string currentSceneName;
    public float[] playerPosition;
    public Dictionary<string, int> caughtFish;
    public int playerMoney;
    
    // Player Data
    public InventoryData playerInventory;
    
    public SaveData()
    {
        currentSceneName = GameManager.Instance.currentScene;
        
        // Save player position
        playerPosition = new float[3];
        playerPosition[0] = GameManager.Instance.Player.transform.position.x;
        playerPosition[1] = GameManager.Instance.Player.transform.position.y;
        playerPosition[2] = GameManager.Instance.Player.transform.position.z;

        caughtFish = GameManager.Instance.Player.caughtFish;
        
        playerMoney = GameManager.Instance.Player.money;
        
        // Save inventory
        playerInventory = new InventoryData(GameManager.Instance.Player.Inventory);
    }
}

[Serializable]
public class InventoryData
{
    // public InventoryItemData[] items;
    public List<InventoryItemData> items = new();
    
    public InventoryData(PlayerInventory inventory)
    {
        // items = new InventoryItemData[inventory.slots.Count];
        for (var i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item != null)
            {
                // items[i] = new InventoryItemData(inventory.slots[i]);
                items.Add(new InventoryItemData(inventory.slots[i]));
            }
        }
    }
}

[Serializable]
public class InventoryItemData
{
    public string itemID;
    public int quantity;
    
    public InventoryItemData(ItemSlot slot)
    {
        if (slot.item is null)
            return;
        itemID = slot.item.Name;
        quantity = slot.count;
    }
}