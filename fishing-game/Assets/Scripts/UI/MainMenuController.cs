using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    
    private void Start()
    {
        startButton.onClick.AddListener(OnStartClick);
        exitButton.onClick.AddListener(OnExitClick);
    }
    
    public void OnStartClick()
    {
        // SceneManager.LoadScene("Scenes/Game");
        // SceneLoader.Instance.LoadScene("Game");
        GameManager.Instance.StartGame();
    }

    public void OnExitClick()
    {
        SaveSystem.SaveGame(1);
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
