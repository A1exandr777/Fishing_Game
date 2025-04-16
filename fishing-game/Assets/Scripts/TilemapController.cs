using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         GetTileBase(Input.mousePosition);
    //     }
    // }

    [CanBeNull]
    public TileBase GetTileBase(Vector2 mousePosition)
    {
        var worldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        var gridPos = tilemap.WorldToCell(worldPos);
        var tile = tilemap.GetTile(gridPos);
        
        return tile;
    }
}
