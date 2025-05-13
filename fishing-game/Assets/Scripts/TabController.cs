using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField] private GameObject[] tabPanels;
    [SerializeField] private Button[] tabButtons;
    
    private int currentTabIndex = 0;

    private void Start()
    {
        // OpenCurrentTab();
        
        for (var i = 0; i < tabButtons.Length; i++)
        {
            var index = i;
            tabButtons[i].onClick.AddListener(() => SwitchTab(index));
        }
    }

    public void SwitchTab(int tabIndex)
    {
        CloseTabs();
        
        tabPanels[tabIndex].SetActive(true);
        
        // Update button states (optional - visual feedback)
        for (var i = 0; i < tabButtons.Length; i++)
        {
            tabButtons[i].interactable = (i != tabIndex);
        }
        
        currentTabIndex = tabIndex;
    }

    public void CloseTabs()
    {
        foreach (var panel in tabPanels)
            panel.SetActive(false);
    }

    public void OpenCurrentTab() => SwitchTab(currentTabIndex);
}
