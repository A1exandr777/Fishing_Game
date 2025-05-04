using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    public GameObject playerPrefab;
    public GameObject canvasPrefab;
    private GameObject currentPlayer;
    private GameObject currentCanvas;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
        SpawnPersistentPlayer();
    }
    
    private void SpawnPersistentPlayer()
    {
        if (currentPlayer == null)
        {
            currentCanvas = Instantiate(canvasPrefab);
            DontDestroyOnLoad(currentCanvas);
            Events.UILoaded.Invoke(currentPlayer.GetComponent<UIController>());
            
            currentPlayer = Instantiate(playerPrefab);
            DontDestroyOnLoad(currentPlayer);
            Events.PlayerSpawned.Invoke(currentPlayer);
            
            // Initialize at default position
            ResetPlayerPosition();
        }
    }

    public void ResetPlayerPosition(Vector3? specificPosition = null)
    {
        if (specificPosition.HasValue)
        {
            currentPlayer.transform.position = specificPosition.Value;
        }
        else
        {
            // Find spawn point in current scene
            currentPlayer.transform.position = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        Events.SceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        Events.SceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        ResetPlayerPosition();
    }
}