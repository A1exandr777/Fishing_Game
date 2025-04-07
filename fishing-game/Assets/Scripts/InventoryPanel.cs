using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<InventorySlot> slots;

    private void Start()
    {
        SetIndex();
        Show();
    }

    private void SetIndex()
    {
        for (var i = 0; i < inventory.slots.Count; i++)
        {
            slots[i].setIndex(i);
        }
    }

    private void Show()
    {
        for (var i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item is null)
            {
                slots[i].Clean();
            }
            else
            {
                slots[i].Set(inventory.slots[i]);
            }
        }
    }
}
