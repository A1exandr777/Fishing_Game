using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    public static ItemSpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] GameObject itemPrefab;

    public void SpawnItem(Vector3 position, Item item, int count)
    {
        var itemObject = Instantiate(itemPrefab, position, Quaternion.identity);
        itemObject.GetComponent<PickUpItem>().Set(item, count);
    }
}
