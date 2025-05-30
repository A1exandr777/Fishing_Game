using UnityEngine;

public enum ItemType
{
    Fish,
    Unknown,
}

[CreateAssetMenu(menuName = "Data/Items/Item")]
public class Item : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public bool Stackable;
    public Sprite Icon;
    public int Price;
    public ItemType Type = ItemType.Unknown;
}
