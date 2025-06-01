using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class FishingRod : ToolObject
{
    [Header("References")]
    public Transform tip;
    public Transform hook;

    public Material lineMaterial;
    
    [Header("Settings")]
    public float castSpeed = 5f;
    public float lineTension = 0.1f;
    public int lineSegments = 20;
    
    private LineRenderer lineRenderer;
    private Vector3[] linePositions;
    private bool isCasting;
    private Vector3 targetPosition;
    private float castProgress;

    public Vector3 lastDirection;
    
    public bool isFishing;
    public bool isThrowing;

    public float throwStrength;

    public bool canFish = true;
    public float sinceLastTimeFishing;
    public float delay = 2f;

    public LootTable fishTable;

    public Item currentFish;


    public override void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegments;
        linePositions = new Vector3[lineSegments];
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = lineMaterial;

        lineRenderer.enabled = false;
        hook.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnDown();
            UIController.Instance.fishingMinigame.StartReeling();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            OnUp();
            UIController.Instance.fishingMinigame.StopReeling();
            // UIController.Instance.throwMinigame.Release();
        }
        
        
        if (!canFish)
        {
            sinceLastTimeFishing += Time.deltaTime;
            if (sinceLastTimeFishing > delay)
            {
                sinceLastTimeFishing = 0f;
                canFish = true;
            }
        }
        
        if (isCasting)
        {
            castProgress += Time.deltaTime * castSpeed;
        
            if (castProgress >= 1f)
            {
                isCasting = false;
                // StartCoroutine(SplashEffect());
            }
        
            UpdateLinePositions();
        }
        else if (hook.gameObject.activeSelf)
        {
            UpdateLinePositions();
        }
    }
    
    private void UpdateLinePositions()
    {
        // Первая точка - конец удочки
        linePositions[0] = tip.position;
    
        // Последняя точка - крючок
        linePositions[lineSegments - 1] = isCasting ? 
            Vector3.Lerp(tip.position, targetPosition, castProgress) : 
            hook.position;
        
        hook.position = isCasting ? linePositions[lineSegments - 1] : targetPosition;
    
        // Промежуточные точки (симуляция провисания)
        for (var i = 1; i < lineSegments - 1; i++)
        {
            var t = (float)i / (lineSegments - 1);
            var basePos = Vector3.Lerp(linePositions[0], linePositions[lineSegments - 1], t);
        
            if (isCasting)
            {
                // Эффект дуги при забросе
                var arcHeight = Mathf.Sin(t * Mathf.PI) * 2f;
                linePositions[i] = basePos + Vector3.up * arcHeight;
            }
            else
            {
                // Небольшое провисание в покое
                var sag = Mathf.Sin(t * Mathf.PI) * lineTension;
                linePositions[i] = basePos + Vector3.down * sag;
            }
        }
    
        lineRenderer.SetPositions(linePositions);
    }

    public void EndFishing(bool success)
    {
        canFish = false;
        
        lineRenderer.enabled = false;
        hook.gameObject.SetActive(false);
        
        isFishing = false;
        GameManager.Instance.Player.AnchorPlayer(false);
        
        if (success)
        {
            GameManager.Instance.Player.Inventory.Add(currentFish, 1);
        }
    }
    
    // public void EndThrow(float strength)
    // {
    //     isFishing = true;
    //
    //     currentFish = ItemDatabase.GetRandomFish();
    //     
    //     UIController.Instance.fishingMinigame.StartMinigame(currentFish, strength, this);
    // }

    public override void OnDown()
    {
        if (!canFish) return;
        if (isFishing || isThrowing || isCasting) return;
        
        animator.Play("Prepare", -1, 0f);

        lineRenderer.enabled = false;
        hook.gameObject.SetActive(false);
        
        // var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        // var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
        // if (!waterTilemap) return;
        //     
        // var worldPos = CameraController.Instance.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        // var gridPos = waterTilemap.WorldToCell(worldPos);
        // var waterTile = waterTilemap.GetTile(gridPos);
        //
        // targetPosition = new Vector3(worldPos.x, worldPos.y, hook.position.z);
        
        if (isFishing || isThrowing) return;
        GameManager.Instance.Player.AnchorPlayer(true);
        UIController.Instance.throwMinigame.StartMinigame(this);
        
        // if (waterTile)
        // {
        //     // GameManager.Instance.FishingController.StartFishing(this);
        // }
    }

    public override void OnUp()
    {
        if (!canFish) return;
        if (isFishing || isThrowing || isCasting) return;
        
        animator.Play("Throw", -1, 0f);

        var strength = UIController.Instance.throwMinigame.StopAndGetResults();
        
        targetPosition = GameManager.Instance.Player.transform.position + lastDirection * (5 * Mathf.Max(strength, 0.2f));
        
        var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
        if (!waterTilemap)
        {
            EndFishing(false);
            return;
        };
        
        var gridPos = waterTilemap.WorldToCell(targetPosition);
        var waterTile = waterTilemap.GetTile(gridPos);

        if (!waterTile)
        {
            EndFishing(false);
            return;
        };
        
        isFishing = true;
        // currentFish = ItemDatabase.GetRandomFish();
        currentFish = fishTable.GetRandomItem();
        
        UIController.Instance.fishingMinigame.StartMinigame(currentFish, strength, this);
    
        // targetPosition = target;
        castProgress = 0f;
        isCasting = true;
        lineRenderer.enabled = true;
        hook.position = targetPosition;
        hook.gameObject.SetActive(true);
    }
    
    public override void UpdateDirection(Vector2 direction)
    {
        lastDirection = new Vector3(direction.x, direction.y, 0);
        var scale = gameObject.transform.localScale;
        scale.x = Mathf.Sign(direction.x);
        gameObject.transform.localScale = scale;
    }
}
