using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public Item item;
    public bool returnable;
    public int count = -1;
}

[CreateAssetMenu(menuName = "Data/Shop Items")]
public class ShopItems : ScriptableObject
{
    public List<ItemInfo> items;
    
    public void Add(ItemInfo itemInfo)
    {
        var item = items.Find(info => info.item == itemInfo.item && info.returnable == itemInfo.returnable);
        if (item != null)
        {
            item.count += itemInfo.count;
        }
        else
        {
            items.Add(itemInfo);
        }
        Events.ShopUpdated.Invoke(this);
    }

    public void Remove(ItemInfo itemInfo)
    {
        items.Remove(itemInfo);
        Events.ShopUpdated.Invoke(this);
    }
}
