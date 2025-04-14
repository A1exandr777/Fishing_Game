using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbar;
    [SerializeField] Item item;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            panel.SetActive(!panel.activeInHierarchy);
            toolbar.SetActive(!toolbar.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.instance.inventory.Add(item, 2);
        }
    }
}
