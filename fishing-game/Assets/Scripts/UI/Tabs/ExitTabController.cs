using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitTabController : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button menuButton;
    
    private void Start()
    {
        exitButton.onClick.AddListener(OnExitClick);
        menuButton.onClick.AddListener(OnMenuClick);
    }
    
    private void OnMenuClick()
    {
        // SceneManager.LoadScene("Scenes/MainMenu");
        SceneLoader.Instance.LoadScene("MainMenu");
    }

    private void OnExitClick()
    {
        SaveSystem.SaveGame(1);
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
