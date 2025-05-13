using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Copy(ItemSlot itemSlot)
    {
        item = itemSlot.item;
        count = itemSlot.count;
    }

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

public class PlayerInventory : MonoBehaviour
{
    public List<ItemSlot> slots;
    public int slotCount = 20;

    private void Awake()
    {
        for (var i = 0; i < slotCount; i++)
        {
            slots.Add(new ItemSlot());
        }
        
        Events.InventoryLoaded.Invoke();
    }
    
    public void Add(Item item, int count = 1)
    {
        GameManager.Instance.Player.caughtFish[item.Name] = 1;
        
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
        
        // GameManager.Instance.UIController.inventoryPanel.Show();
        Events.ItemAdded.Invoke();
    }
}