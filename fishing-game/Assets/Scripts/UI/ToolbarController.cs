using System;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public int toolbarSize = 3;
    private int selectedItem;

    public Action<int> onChange;

    private void Update()
    {
        var delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            selectedItem += Math.Sign(delta);
            selectedItem = selectedItem < 0 ? toolbarSize - 1 : (selectedItem >= toolbarSize ? 0 : selectedItem);
            onChange?.Invoke(selectedItem);
        }
    }

    public void Set(int index)
    {
        selectedItem = index;
    }
}
