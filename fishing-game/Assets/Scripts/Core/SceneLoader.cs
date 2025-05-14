using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    
    [Header("Settings")]
    public float fadeDuration = 0.5f;
    
    private string sceneToLoad;
    private Vector2? positionToLoad;
    
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
    
    public void LoadScene(string sceneName, Vector2? position = null, bool fadeIn = true)
    {
        // GameManager.Instance.playerPositionBeforeSceneChange = GameManager.Instance.Player.transform.position;
        sceneToLoad = sceneName;
        positionToLoad = position;
        // SceneManager.LoadScene(sceneToLoad);
        // GameManager.Instance.Player.transform.position = position;
        // Events.SceneLoaded.Invoke();
        StartCoroutine(FadeAndLoadScene(fadeIn));
    }
    
    private IEnumerator FadeAndLoadScene(bool fadeIn)
    {
        var timer = 0f;
        
        // Fade in
        if (fadeIn)
        {
            while (timer < fadeDuration)
            {
                SetAlpha(Mathf.Lerp(0f, 1f, timer / fadeDuration));
                timer += Time.deltaTime;
                yield return null;
            }
        }
        SetAlpha(1f);

        if (sceneToLoad is "Game" or "MainMenu")
        {
            GameManager.Instance.UIController.SwitchUI(sceneToLoad);
        }
        
        // Load scene async
        var asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        // After scene loads
        SceneLoaded();
        
        // Fade out
        timer = 0f;
        while (timer < fadeDuration)
        {
            SetAlpha(Mathf.Lerp(1f, 0f, timer / fadeDuration));
            timer += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f);
    }
    
    private void SceneLoaded()
    {
        // GameManager.Instance.currentScene = sceneToLoad;
        // GameManager.Instance.Player.transform.position = positionToLoad ?? Vector2.zero;
    }

    private void SetAlpha(float alpha)
    {
        var currentColor = GameManager.Instance.UIController.fade.color;
        currentColor.a = Mathf.Clamp(alpha, 0f, 1f);
        GameManager.Instance.UIController.fade.color = currentColor;
    }
}