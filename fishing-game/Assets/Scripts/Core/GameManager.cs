using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Cutscene startCutscene;

    public ItemList startItems;
    
    public ItemDragController dragController;
    
    [Header("Game Settings")]
    public bool isGamePaused;
    
    [Header("Player References")]
    public PlayerController Player;
    public UIController UIController;
    public CutsceneController CutsceneController;
    public ShopController ShopController;
    public DialogueController DialogueController;
    
    [Header("Scene Management")]
    public string currentScene;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);

        // UIController.Init();
        SceneLoader.Instance.LoadScene("MainMenu", fadeIn: false);
        // SoundController.Instance.StartBackgroundMusic();
    }

    public void StartGame()
    {
        if (SaveSystem.SaveExists(1))
        {
            LoadPlayerData();
            SceneLoader.Instance.LoadScene("Game");
        }
        else
        {
            GiveStartItems();
            
            CutsceneController.PlayCutscene(
                startCutscene,
                fadeOut: false,
                callback: () => { SceneLoader.Instance.LoadScene("Game", fadeIn: false); });
        }
    }

    private void GiveStartItems()
    {
        foreach (var itemEntry in startItems.items)
        {
            Player.Inventory.Add(itemEntry.item, itemEntry.count); 
        }
    }

    private void LoadPlayerData()
    {
        try
        {
            var data = SaveSystem.LoadGame(1);
            if (data == null) return;
        
            foreach (var item in data.playerInventory.items)
            {
                Player.Inventory.Add(ItemDatabase.GetItem(item.itemID), item.quantity);
            }

            Player.caughtFish = data.caughtFish;
            Player.money = data.playerMoney;
            
            DayCycleController.Instance.SetTimeOfDay(data.timeOfDay);
            
            var pos = data.playerPosition;
            
            Player.SetPosition(new Vector3(pos[0], pos[1], pos[2]));
        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong when loading player data.");
        }
    }
    
    public void TogglePause(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0 : 1;
    }
}
