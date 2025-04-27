using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public static FishingController instance;

    public ThrowMinigame throwMinigame;
    public FishingMinigame minigame;
    public Item fish;
    public bool isFishing;
    public bool isThrowing;
    
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
        if (isFishing || isThrowing) return;
        isThrowing = true;
        GameManager.instance.characterController.AnchorPlayer(true);
        // throwMinigame.StartMinigame();
        EndThrow(1f);
    }

    public void EndThrow(float score)
    {
        isThrowing = false;
        isFishing = true;
        minigame.StartMinigame(fish, score);
    }

    public void EndFishing(bool success)
    {
        isFishing = false;
        GameManager.instance.characterController.AnchorPlayer(false);
        if (success)
        {
            GameManager.instance.inventory.Add(fish, 1);
        }
    }
}
