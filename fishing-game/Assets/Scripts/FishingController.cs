using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public static FishingController instance;
    
    public FishingMinigame minigame;
    public Item fish;
    public bool isFishing;
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            minigame.StartReeling();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            minigame.StopReeling();
        }
    }

    public void StartFishing()
    {
        if (isFishing) return;
        Debug.Log("Started fishing");
        isFishing = true;
        GameManager.instance.characterController.AnchorPlayer(true);
        minigame.StartMinigame(fish);
    }

    public void EndFishing(bool success)
    {
        Debug.Log($"Fishing successful: {success}");
        isFishing = false;
        GameManager.instance.characterController.AnchorPlayer(false);
        if (success)
        {
            GameManager.instance.inventory.Add(fish, 1);
        }
    }
}
