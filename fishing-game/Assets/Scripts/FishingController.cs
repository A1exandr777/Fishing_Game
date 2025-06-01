using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public UIController uiController;
    
    // public ThrowMinigame throwMinigame;
    // public FishingMinigame fishingMinigame;
    // public bool isFishing;
    // public bool isThrowing;

    private FishingRod currentRod;

    private Item fish;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            uiController.fishingMinigame.StartReeling();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            uiController.fishingMinigame.StopReeling();
            // uiController.throwMinigame.Release();
        }
    }

    public void StartFishing(FishingRod fishingRod)
    {
        if (fishingRod.isFishing || fishingRod.isThrowing) return;
        currentRod = fishingRod;
        // isThrowing = true;
        GameManager.Instance.Player.AnchorPlayer(true);
        uiController.throwMinigame.StartMinigame(currentRod);
        // EndThrow(1f);
    }

    public void EndThrow(float strength)
    {
        // isThrowing = false;
        currentRod.isFishing = true;
        currentRod.throwStrength = strength;

        fish = ItemDatabase.GetRandomFish();
        
        uiController.fishingMinigame.StartMinigame(fish, strength, currentRod);
    }

    public void EndFishing(bool success)
    {
        currentRod.isFishing = false;
        GameManager.Instance.Player.AnchorPlayer(false);
        currentRod.EndFishing(success);
        if (success)
        {
            GameManager.Instance.Player.Inventory.Add(fish, 1);
        }
    }

    // private void AttachUIController(UIController controller)
    // {
    //     uiController = controller;
    // }
    //
    // private void OnEnable()
    // {
    //     Events.UILoaded += AttachUIController;
    // }
    //
    // private void OnDisable()
    // {
    //     Events.UILoaded -= AttachUIController;
    // }
}
