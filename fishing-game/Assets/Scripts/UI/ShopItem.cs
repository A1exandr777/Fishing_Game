using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI itemName;
    public Image returnIcon;

    public ItemInfo shopItemInfo;

    public UnityAction<ShopItem> onClick;
    
    public void Set(ItemInfo itemInfo)
    {
        shopItemInfo = itemInfo;
        itemIcon.sprite = itemInfo.item.Icon;
        itemName.text = itemInfo.item.Name;
        itemPrice.text = itemInfo.item.Price.ToString();
        
        returnIcon.gameObject.SetActive(itemInfo.returnable);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke(this);
        
        // if (!GameManager.Instance.Player.EnoughMoney(shopItemInfo.item.Price))
        //     return;
        // GameManager.Instance.Player.TakeMoney(shopItemInfo.item.Price);
        // GameManager.Instance.Player.Inventory.Add(shopItemInfo.item);
        // Debug.Log(GameManager.Instance.Player.money);
    }
}
