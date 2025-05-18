using System;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem Instance;

    public Tooltip tooltip;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show(string content)
    {
        Instance.tooltip.SetText(content);
        Instance.tooltip.gameObject.SetActive(true);
    }
    
    public static void Hide()
    {
        Instance.tooltip.gameObject.SetActive(false);
    }
}
