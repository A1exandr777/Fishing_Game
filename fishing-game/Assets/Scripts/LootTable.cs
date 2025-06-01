using System.Linq;
using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public Item item;
    [Range(0, 100)] public float dropRate;
}

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    public LootEntry[] lootEntries;

    public Item GetRandomItem()
    {
        var totalWeight = lootEntries.Sum(entry => entry.dropRate);
        var randomValue = Random.Range(0f, totalWeight);
        var currentWeight = 0f;

        foreach (var entry in lootEntries)
        {
            currentWeight += entry.dropRate;
            if (randomValue <= currentWeight)
            {
                return entry.item;
            }
        }

        return null;
    }
}