using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image highlight;

    public UnityAction<InventorySlot> onClick;

    public int index;

    public void SetIndex(int i)
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
        onClick.Invoke(this);
        
        // var itemPanel = transform.parent.parent.parent.GetComponent<InventoryTabController>();
        // itemPanel.OnClick(index);
        
        // var inventory = GameManager.instance.inventory;
        // GameManager.instance.dragController.OnClick(inventory.slots[index]);
        // transform.parent.GetComponent<InventoryPanel>().Show();
    }
}
