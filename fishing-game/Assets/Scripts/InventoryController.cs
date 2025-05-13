using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject toolbar;
    [SerializeField] Item[] items;

    public Cutscene cutscene;

    public TabController tabController;
    private bool open;

    // private void Start()
    // {
    //     tabController = panel.GetComponent<TabController>();
    // }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            open = !open;
            
            panel.SetActive(open);
            toolbar.SetActive(!open);

            if (open)
                tabController.OpenCurrentTab();
            else
                tabController.CloseTabs();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (var item in items)
            {
                GameManager.Instance.Inventory.Add(item, 1);
            }
            
            // GameManager.Instance.CutsceneController.PlayCutscene(cutscene);
        }
    }
}
