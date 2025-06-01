using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ShopController : MonoBehaviour
{
    public TextMeshProUGUI moneyLabel;
    
    public Button closeButton;
    
    public GameObject npcInventory;
    public GameObject shopItemPrefab;
    
    public GameObject playerInventory;
    public List<InventorySlot> slots;
    public GameObject slotPrefab;
    public int slotCount = 20;
    
    private ShopItems currentShop;

    private void Awake()
    {
        closeButton.onClick.AddListener(Close);
    }

    public void OpenShop(ShopItems shop)
    {
        currentShop = shop;
        
        gameObject.SetActive(true);
        LoadNpcInventory(currentShop);
        LoadPlayerInventory();
        
        UpdateMoney(GameManager.Instance.Player.money);

        Events.ItemAdded += LoadPlayerInventory;
        Events.ShopUpdated += LoadNpcInventory;
        Events.MoneyChanged += UpdateMoney;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        
        Events.ItemAdded -= LoadPlayerInventory;
        Events.ShopUpdated -= LoadNpcInventory;
        Events.MoneyChanged -= UpdateMoney;
    }
    
    public void UpdateMoney(int count)
    {
        moneyLabel.text = count.ToString();
    }
    
    public void LoadNpcInventory(ShopItems shop)
    {
        foreach (Transform child in npcInventory.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var itemInfo in shop.items)
        {
            var shopItem = Instantiate(shopItemPrefab, npcInventory.transform);
            var itemController = shopItem.GetComponent<ShopItem>();
            itemController.Set(itemInfo);
            itemController.onClick += OnShopItemClick;
            
            shopItem.GetComponent<TooltipTrigger>().content = itemInfo.item.Name;
        }
    }

    public void LoadPlayerInventory()
    {
        foreach (Transform child in playerInventory.transform)
        {
            Destroy(child.gameObject);
        }
        
        for (var i = 0; i < slotCount; i++)
        {
            var slotObject = Instantiate(slotPrefab, playerInventory.transform);
            var slot = slotObject.GetComponent<InventorySlot>();
            slot.SetIndex(i);
            slot.onClick += OnSlotClick;
            slots.Add(slot);
            
            if (GameManager.Instance.Player.Inventory.slots[i].item is null)
            {
                slot.Clean();
            }
            else
            {
                slot.Set(GameManager.Instance.Player.Inventory.slots[i]);
            }

            if (i < 3)
            {
                slot.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f, 1f);
            }
        }
    }

    public void OnShopItemClick(ShopItem shopItem)
    {
        if (!GameManager.Instance.Player.EnoughMoney(shopItem.shopItemInfo.item.Price))
            return;
        GameManager.Instance.Player.TakeMoney(shopItem.shopItemInfo.item.Price);
        GameManager.Instance.Player.Inventory.Add(shopItem.shopItemInfo.item, Math.Max(shopItem.shopItemInfo.count, 1));
        if (shopItem.shopItemInfo.returnable)
        {
            currentShop.Remove(shopItem.shopItemInfo);
        }
        // Debug.Log(GameManager.Instance.Player.money);
    }
    
    public void OnSlotClick(InventorySlot slot)
    {
        var itemSlot = GameManager.Instance.Player.Inventory.slots[slot.index];
        if (itemSlot.item == null)
            return;
        
        // currentShop.Add(itemSlot.item);
        currentShop.Add(new ItemInfo() {item = itemSlot.item, returnable = true, count = itemSlot.count});
        
        GameManager.Instance.Player.GiveMoney((int)(itemSlot.item.Price * 0.5f) * itemSlot.count);
        GameManager.Instance.Player.Inventory.Remove(slot.index);
        // Debug.Log(GameManager.Instance.Player.money);
    }
}
