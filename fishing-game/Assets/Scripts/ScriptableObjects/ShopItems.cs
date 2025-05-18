using System;
using System.Collections.Generic;
using UnityEngine;

// public class ItemInfo
// {
//     public Item item;
// }

[CreateAssetMenu(menuName = "Data/Shop Items")]
public class ShopItems : ScriptableObject
{
    public List<Item> items;
    
    public void Add(Item item)
    {
        items.Add(item);
        Events.ShopUpdated.Invoke(this);
    }
}
