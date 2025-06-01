using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public string content;
    
    public void OnPointerEnter(PointerEventData pointerData)
    {
        if (content == "")
            return;
        TooltipSystem.Show(content);
    }
    
    public void OnPointerExit(PointerEventData pointerData)
    {
        TooltipSystem.Hide();
    }

    private void OnDestroy()
    {
        TooltipSystem.Hide();
    }
}
