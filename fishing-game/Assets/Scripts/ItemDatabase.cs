using UnityEngine;
using System.Collections.Generic;
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
        var loadedItems = Resources.LoadAll<Item>("Fish");

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

        Debug.LogError($"Item '{itemName}' not found!");
        return null;
    }

    public static Item GetRandomFish()
    {
        if (_itemList == null) Initialize();
        
        var rng = new Random();
        return _itemList[rng.Next(_itemList.Count)];
    }

    public static List<Item> GetAllFish()
    {
        if (_itemList == null) Initialize();
        return _itemList;
    }
}