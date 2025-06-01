using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterInteractionController : MonoBehaviour
{
    public float offsetDistance = 1f;
    public float interactionRadius = 1.2f;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (GameManager.Instance.UIController.GetComponent<InventoryController>().open)
            return;

        // var tool = GameManager.Instance.Player.currentTool;
        // if (tool)
        // {
        //     if (Mouse.current.leftButton.wasPressedThisFrame)
        //     {
        //         tool.GetComponent<ToolObject>().OnDown();
        //     }
        //     if (Mouse.current.leftButton.wasReleasedThisFrame)
        //     {
        //         tool.GetComponent<ToolObject>().OnUp();
        //     }
        // }
        
        // if (Mouse.current.leftButton.wasPressedThisFrame && !GameManager.Instance.FishingController.isFishing)
        // {
        // }
        //
        // if (Mouse.current.leftButton.wasReleasedThisFrame && !GameManager.Instance.FishingController.isFishing)
        // {
        //     
        // }
    }

    // private void UseTool()
    // {
    //     var position = rigidbody2d.position + character.lastMotionVector * offsetDistance;
    //     var colliders = Physics2D.OverlapCircleAll(position, interactionRadius);
    //     foreach (var collider in colliders)
    //     {
    //         ToolHit hit = collider.GetComponent<ToolHit>();
    //         if (hit != null)
    //         {
    //             hit.Hit();
    //             return;
    //         }
    //     }
    // }
}
