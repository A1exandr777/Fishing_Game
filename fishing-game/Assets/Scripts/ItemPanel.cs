using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventorySlot> slots;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        Show();
    }

    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (var i = 0; i < inventory.slots.Count && i < slots.Count; i++)
        {
            slots[i].setIndex(i);
        }
    }

    public void Show()
    {
        for (var i = 0; i < inventory.slots.Count && i < slots.Count; i++)
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

    public virtual void OnClick(int index)
    {
        
    }
}