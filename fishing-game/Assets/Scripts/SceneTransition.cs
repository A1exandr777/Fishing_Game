using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public Vector2 position;
    // public Vector2 playerPosition;
    // public VectorValue savedPosition;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // savedPosition.initialValue = playerPosition;
            SceneLoader.Instance.LoadScene(sceneToLoad, position);
            // SceneManager.LoadScene(sceneToLoad);
        }
    }
}
