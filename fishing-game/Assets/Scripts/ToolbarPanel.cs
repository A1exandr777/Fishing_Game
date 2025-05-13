using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToolbarPanel : ItemPanel
{
    [SerializeField] private ToolbarController toolbar;
    private int current;

    private void Awake()
    {
        Events.InventoryLoaded += ToolbarInit;
    }

    public void ToolbarInit()
    {
        Init();
        
        Show();
        toolbar.onChange += Highlight;
        Highlight(0);
    }
    
    public override void OnClick(int index)
    {
        toolbar.Set(index);
        Highlight(index);
    }

    private void Highlight(int index)
    {
        slots[current].Highlight(false);
        current = index;
        slots[current].Highlight(true);
    }
}
