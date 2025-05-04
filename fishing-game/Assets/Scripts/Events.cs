using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static UnityAction SceneLoaded;
    
    public static UnityAction<GameObject> PlayerSpawned;

    public static UnityAction<UIController> UILoaded;
}