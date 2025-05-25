using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static UnityAction SceneLoaded;

    public static UnityAction ItemAdded;
    public static UnityAction InventoryLoaded;
    public static UnityAction<int> MoneyChanged;
    public static UnityAction<ShopItems> ShopUpdated;
    public static UnityAction<int> ToolbarScroll;
    
    public static UnityAction<GameObject> PlayerSpawned;
    public static UnityAction<UIController> UILoaded;
}