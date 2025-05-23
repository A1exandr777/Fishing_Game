using System;
using System.Collections.Generic;
using UnityEngine;

// [Serializable]
// public class ItemSlot
// {
//     public Item item;
//     public int count;
//
//     public void Copy(ItemSlot slot)
//     {
//         item = slot.item;
//         count = slot.count;
//     }
//
//     public void Set(Item item, int count)
//     {
//         this.item = item;
//         this.count = count;
//     }
//
//     public void Clear()
//     {
//         item = null;
//         count = 0;
//     }
// }

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Item item, int count = 1)
    {
        if (item.Stackable)
        {
            var itemSlot = slots.Find(x => x.item == item);
            if (itemSlot is not null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item is null);
                if (itemSlot is not null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            var itemSlot = slots.Find(x => x.item is null);
            if (itemSlot is not null)
            {
                itemSlot.item = item;
            }
        }
    }
}
