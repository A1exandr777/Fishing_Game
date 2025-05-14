using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    // public ItemContainer inventory;
    public GameObject panel;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;
    public int slotCount = 20;

    // private void Awake()
    // {
    //     Events.InventoryLoaded += Init;
    // }

    public void Init()
    {
        for (var i = 0; i < slotCount; i++)
        {
            var slotObject = Instantiate(slotPrefab, panel.transform);
            var slot = slotObject.GetComponent<InventorySlot>();
            slot.setIndex(i);
            slots.Add(slot);
            
        }
        
        // SetIndex();
        Show();
        Events.ItemAdded += Show;
    }

    // private void OnEnable()
    // {
    //     Show();
    // }

    // private void SetIndex()
    // {
    //     for (var i = 0; i < GameManager.Instance.Inventory.slots.Count && i < slots.Count; i++)
    //     {
    //         slots[i].setIndex(i);
    //     }
    // }

    public void Show()
    {
        for (var i = 0; i < GameManager.Instance.Player.Inventory.slots.Count && i < slots.Count; i++)
        {
            if (GameManager.Instance.Player.Inventory.slots[i].item is null)
            {
                slots[i].Clean();
            }
            else
            {
                slots[i].Set(GameManager.Instance.Player.Inventory.slots[i]);
            }
        }
    }

    public virtual void OnClick(int index)
    {
        
    }
}