using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // public GameObject player;
    // public CharacterController2D characterController;
    public ItemContainer inventory;
    public ItemDragController dragController;
    public TilemapController tilemapController;
    
    [Header("Game Settings")]
    public bool isGamePaused = false;
    
    [Header("Player References")]
    public PlayerController Player;
    public FishingController FishingController;
    public UIController UIController;
    // public PlayerInventory inventory;
    
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
    }
    
    public void TogglePause(bool pause)
    {
        isGamePaused = pause;
        Time.timeScale = pause ? 0 : 1;
    }
}
