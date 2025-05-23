using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public bool Stackable;
    public Sprite Icon;
    public int Price;
}
