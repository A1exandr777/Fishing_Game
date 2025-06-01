using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemEntry
{
    public Item item;
    public int count;
}

[CreateAssetMenu(menuName = "Data/Item List")]
public class ItemList : ScriptableObject
{
    public List<ItemEntry> items;
}
