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

    public Item shopItem;

    public UnityAction<ShopItem> onClick;
    
    public void Set(Item item)
    {
        shopItem = item;
        itemIcon.sprite = item.Icon;
        itemName.text = item.Name;
        itemPrice.text = item.Price.ToString();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Invoke(this);
        
        if (!GameManager.Instance.Player.EnoughMoney(shopItem.Price))
            return;
        GameManager.Instance.Player.TakeMoney(shopItem.Price);
        GameManager.Instance.Player.Inventory.Add(shopItem);
        Debug.Log(GameManager.Instance.Player.money);
    }
}
