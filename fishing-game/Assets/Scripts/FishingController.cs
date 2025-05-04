using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour
{
    public UIController uiController;
    
    // public ThrowMinigame throwMinigame;
    // public FishingMinigame fishingMinigame;
    public Item fish;
    public bool isFishing;
    public bool isThrowing;
    
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
        }
    }

    public void StartFishing()
    {
        if (isFishing || isThrowing) return;
        isThrowing = true;
        GameManager.Instance.Player.AnchorPlayer(true);
        // throwMinigame.StartMinigame();
        EndThrow(1f);
    }

    public void EndThrow(float score)
    {
        isThrowing = false;
        isFishing = true;
        uiController.fishingMinigame.StartMinigame(fish, score);
    }

    public void EndFishing(bool success)
    {
        isFishing = false;
        GameManager.Instance.Player.AnchorPlayer(false);
        if (success)
        {
            GameManager.Instance.inventory.Add(fish, 1);
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
