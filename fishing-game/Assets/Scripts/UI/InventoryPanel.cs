using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    private void Awake()
    {
        Events.InventoryLoaded += Init;
    }
    
    public override void OnClick(int index)
    {
        GameManager.Instance.dragController.OnClick(GameManager.Instance.Player.Inventory.slots[index]);
        Show();
    }
}
