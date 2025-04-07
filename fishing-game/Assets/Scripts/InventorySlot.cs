using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;

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
}
