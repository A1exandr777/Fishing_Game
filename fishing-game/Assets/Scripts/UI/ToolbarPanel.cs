using System.Collections.Generic;
using UnityEngine;

public class ToolbarPanel : MonoBehaviour
{
    public ToolbarController toolbar;
    public GameObject panel;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;
    public int slotCount = 3;
    
    private int current;

    private void Awake()
    {
        Events.InventoryLoaded += Init;
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
        }
        
        Highlight(0);
        
        Show();
        Events.ToolbarScroll += Highlight;
        Events.ItemAdded += Show;
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

    private void Highlight(int index)
    {
        slots[current].Highlight(false);
        current = index;
        slots[current].Highlight(true);
    }
    
    public void OnSlotClick(InventorySlot slot)
    {
        toolbar.Set(slot.index);
        Highlight(slot.index);
    }
}
