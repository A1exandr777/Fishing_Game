using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemDragController : MonoBehaviour
{
    [SerializeField] ItemSlot slot;
    [SerializeField] GameObject itemIcon;
    RectTransform iconTransform;
    Image itemIconImage;

    private void Start()
    {
        slot = new ItemSlot();
        iconTransform = itemIcon.GetComponent<RectTransform>();
        itemIconImage = itemIcon.GetComponent<Image>();
    }

    private void Update()
    {
        if (itemIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;
        }
    }

    internal void OnClick(ItemSlot itemSlot)
    {
        if (slot.item is null)
        {
            slot.Copy(itemSlot);
            itemSlot.Clear();
        }
        else
        {
            var item = itemSlot.item;
            var count = itemSlot.count;

            itemSlot.Copy(slot);
            slot.Set(item, count);
        }
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (slot.item is null)
        {
            itemIcon.SetActive(false);
        }
        else
        {
            itemIcon.SetActive(true);
            itemIconImage.sprite = slot.item.Icon;
        }
    }
}
