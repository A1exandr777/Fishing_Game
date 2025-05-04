using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CharacterInteractionController : MonoBehaviour
{
    PlayerController character;
    Rigidbody2D rigidbody2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float interactionRadius = 1.2f;

    private void Awake()
    {
        character = GetComponent<PlayerController>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // UseTool();
            var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
            var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
            if (!waterTilemap) return;
            
            var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var gridPos = waterTilemap.WorldToCell(worldPos);
            var waterTile = waterTilemap.GetTile(gridPos);

            // var waterTile = GameManager.Instance.tilemapController.GetTileBase(Input.mousePosition);
            if (waterTile)
            {
                GameManager.Instance.FishingController.StartFishing();
            }
        }
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
