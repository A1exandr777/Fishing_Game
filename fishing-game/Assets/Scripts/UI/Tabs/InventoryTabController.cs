using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabController : MonoBehaviour
{
    public TextMeshProUGUI moneyLabel;
    
    public GameObject panel;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;
    public int slotCount = 20;
    
    private void Awake()
    {
        Events.InventoryLoaded += Init;
        Events.MoneyChanged += UpdateMoney;
    }

    public void Init()
    {
        for (var i = 0; i < slotCount; i++)
        {
            var slotObject = Instantiate(slotPrefab, panel.transform);
            var slot = slotObject.GetComponent<InventorySlot>();
            slot.SetIndex(i);
            slot.onClick += OnSlotClick;
            slots.Add(slot);

            if (i < 3)
            {
                slot.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            }
        }
        
        Show();
        Events.ItemAdded += Show;
    }

    public void UpdateMoney(int count)
    {
        moneyLabel.text = count.ToString();
    }

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
    
    public void OnSlotClick(InventorySlot slot)
    {
        GameManager.Instance.dragController.OnClick(GameManager.Instance.Player.Inventory.slots[slot.index]);
        Show();
    }
}
