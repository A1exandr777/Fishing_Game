using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    public ThrowMinigame throwMinigame;
    public FishingMinigame fishingMinigame;
    // public InventoryPanel inventoryPanel;
    public ToolbarPanel toolbarPanel;
    public GameObject gameCanvas;
    public GameObject menuCanvas;
    public Image fade;

    public string currentUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    // public void Init()
    // {
    //     inventoryPanel.Init();
    //     toolbarPanel.Init();
    //     toolbarPanel.ToolbarInit();
    // }

    public void SwitchUI(string newUI)
    {
        gameCanvas.SetActive(false);
        menuCanvas.SetActive(false);
        
        currentUI = newUI;
        
        switch (currentUI)
        {
            case "Game":
                gameCanvas.SetActive(true);
                break;
            case "MainMenu":
                menuCanvas.SetActive(true);
                break;
        }
    }
}
