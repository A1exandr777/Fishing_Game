using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image highlight;

    int index;

    public void setIndex(int i)
    {
        index = i;
    }

    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.Icon;

        text.gameObject.SetActive(slot.item.Stackable);
        if (slot.item.Stackable)
        {
            text.text = slot.count.ToString();
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void Highlight(bool state)
    {
        highlight.gameObject.SetActive(state);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var itemPanel = transform.parent.GetComponent<ItemPanel>();
        itemPanel.OnClick(index);
        // var inventory = GameManager.instance.inventory;
        // GameManager.instance.dragController.OnClick(inventory.slots[index]);
        // transform.parent.GetComponent<InventoryPanel>().Show();
    }
}
