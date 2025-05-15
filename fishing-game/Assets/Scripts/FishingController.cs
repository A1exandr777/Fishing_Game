using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public UIController uiController;
    
    // public ThrowMinigame throwMinigame;
    // public FishingMinigame fishingMinigame;
    public bool isFishing;
    public bool isThrowing;

    private Item fish;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            uiController.fishingMinigame.StartReeling();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            uiController.fishingMinigame.StopReeling();
            uiController.throwMinigame.Release();
        }
    }

    public void StartFishing()
    {
        if (isFishing || isThrowing) return;
        // isThrowing = true;
        GameManager.Instance.Player.AnchorPlayer(true);
        uiController.throwMinigame.StartMinigame();
        // EndThrow(1f);
    }

    public void EndThrow(float strength)
    {
        // isThrowing = false;
        isFishing = true;

        fish = ItemDatabase.GetRandomFish();
        
        uiController.fishingMinigame.StartMinigame(fish, strength);
    }

    public void EndFishing(bool success)
    {
        isFishing = false;
        GameManager.Instance.Player.AnchorPlayer(false);
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
