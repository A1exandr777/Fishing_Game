using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : ItemPanel
{
    public override void OnClick(int index)
    {
        GameManager.instance.dragController.OnClick(inventory.slots[index]);
        Show();
    }
}
