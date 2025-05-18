using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Texture2D hoverCursor;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    public ShopItems shop;
    
    public void OnMouseEnter()
    {
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, hotSpot, cursorMode);
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnMouseDown()
    {
        GameManager.Instance.ShopController.OpenShop(shop);
    }
}
