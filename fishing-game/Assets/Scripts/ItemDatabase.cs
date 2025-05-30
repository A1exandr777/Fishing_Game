using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Random = System.Random;

public static class ItemDatabase
{
    private static Dictionary<string, Item> _items;
    private static List<Item> _itemList;
    
    public static void Initialize()
    {
        if (_items != null) return;

        _items = new Dictionary<string, Item>();
        _itemList = new List<Item>();
        var loadedItems = Resources.LoadAll<Item>("");
        // var allItems = Resources.LoadAll<Item>("");
        // var filteredItems = allItems.Where(obj => AssetDatabase.GetAssetPath(obj).Contains(targetFolder)).ToArray();

        foreach (var item in loadedItems)
        {
            _items.Add(item.Name, item);
            _itemList.Add(item);
        }
    }
    
    public static Item GetItem(string itemName)
    {
        if (_items == null) Initialize();

        if (_items.TryGetValue(itemName, out var item))
            return item;

        Debug.LogError($"Item \"{itemName}\" not found!");
        return null;
    }

    public static Item GetRandomFish()
    {
        var rng = new Random();
        return GetAllFish()[rng.Next(_itemList.Count)];
    }

    public static List<Item> GetAllFish()
    {
        if (_itemList == null) Initialize();
        return _itemList.Where(item => item.Type == ItemType.Fish).ToList();
    }
}