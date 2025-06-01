using System;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public int toolbarSize = 3;
    public int selectedItem;

    private void Update()
    {
        var delta = Input.mouseScrollDelta.y;
        if (delta == 0) return;
        
        selectedItem += Math.Sign(delta);
        selectedItem = selectedItem < 0 ? toolbarSize - 1 : (selectedItem >= toolbarSize ? 0 : selectedItem);
        Events.ToolbarScroll?.Invoke(selectedItem);
    }

    public void Set(int index)
    {
        selectedItem = index;
        Events.ToolbarScroll?.Invoke(selectedItem);
    }
}
