using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Cutscene startCutscene;
    
    // public GameObject player;
    // public CharacterController2D characterController;
    // public ItemContainer inventory;
    public ItemDragController dragController;
    public TilemapController tilemapController;
    
    [Header("Game Settings")]
    public bool isGamePaused;
    
    [Header("Player References")]
    public PlayerController Player;
    public FishingController FishingController;
    public UIController UIController;
    public CutsceneController CutsceneController;
    public ShopController ShopController;
    public DialogueController DialogueController;
    public SoundController SoundController;
    
    [Header("Scene Management")]
    public string currentScene;
    // public Vector3 playerPositionBeforeSceneChange;

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
        SoundController.StartBackgroundMusic();
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
            SceneLoader.Instance.LoadScene("Game", fadeIn: false);
            
            // CutsceneController.PlayCutscene(
            //     startCutscene,
            //     fadeOut: false,
            //     callback: () => { SceneLoader.Instance.LoadScene("Game", fadeIn: false); });
        }
    }

    private void LoadPlayerData()
    {
        var data = SaveSystem.LoadGame(1);
        if (data == null) return;
        
        
        foreach (var item in data.playerInventory.items)
        {
            Player.Inventory.Add(ItemDatabase.GetItem(item.itemID), item.quantity);
        }

        Player.caughtFish = data.caughtFish;
        // Player.health = data.playerHealth;
        // Player.money = data.playerMoney;
            
        var pos = data.playerPosition;
            
        Player.SetPosition(new Vector3(pos[0], pos[1], pos[2]));
    }
    
    public void TogglePause(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0 : 1;
    }
}
