using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabController : MonoBehaviour
{
    public GameObject panel;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;
    public int slotCount = 20;
    
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
            slot.setIndex(i);
            slots.Add(slot);

            if (i < 3)
            {
                slot.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            }
        }
        
        Show();
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
    
    public void OnClick(int index)
    {
        GameManager.Instance.dragController.OnClick(GameManager.Instance.Player.Inventory.slots[index]);
        Show();
    }
}
