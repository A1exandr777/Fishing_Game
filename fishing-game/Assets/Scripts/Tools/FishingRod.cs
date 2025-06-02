using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public enum FishingState
{
    None,
    Prepared,
    Throwing,
    Waiting,
    Fishing,
}

public class FishingRod : ToolObject
{
    [Header("References")]
    public Transform tip;
    public Transform hook;

    public GameObject indicator;

    public Material lineMaterial;
    
    [Header("Settings")]
    public float castSpeed = 5f;
    public float lineTension = 0.1f;
    public int lineSegments = 20;
    
    private LineRenderer lineRenderer;
    private Vector3[] linePositions;
    private Vector3 targetPosition;
    private float castProgress;

    public Vector3 lastDirection;

    public float throwStrength;

    public bool canFish = true;
    public float sinceLastTimeFishing;
    public float delay = 2f;

    private float currentWait;
    private float waitTime;
    private bool canCatch;

    private float timeWindow = 0.5f;

    public LootTable fishTable;

    public Item currentFish;

    private FishingState currentState = FishingState.None;


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
            if (currentState == FishingState.Fishing)
                UIController.Instance.fishingMinigame.StartReeling();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            OnUp();
            if (currentState == FishingState.Fishing)
                UIController.Instance.fishingMinigame.StopReeling();
        }

        if (currentState == FishingState.Waiting)
        {
            currentWait += Time.deltaTime;
            canCatch = Mathf.Abs(currentWait - waitTime) <= timeWindow;   
        }
        
        indicator.SetActive(canCatch);
        
        
        if (!canFish)
        {
            sinceLastTimeFishing += Time.deltaTime;
            if (sinceLastTimeFishing > delay)
            {
                sinceLastTimeFishing = 0f;
                canFish = true;
            }
        }
        
        if (currentState == FishingState.Throwing)
        {
            castProgress += Time.deltaTime * castSpeed;
        
            if (castProgress >= 1f)
            {
                currentState = FishingState.Waiting;
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
        linePositions[0] = tip.position;
        
        linePositions[lineSegments - 1] = currentState == FishingState.Throwing ? 
            Vector3.Lerp(tip.position, targetPosition, castProgress) : 
            hook.position;
        
        hook.position = currentState == FishingState.Throwing ? linePositions[lineSegments - 1] : targetPosition;
        
        for (var i = 1; i < lineSegments - 1; i++)
        {
            var t = (float)i / (lineSegments - 1);
            var basePos = Vector3.Lerp(linePositions[0], linePositions[lineSegments - 1], t);
        
            if (currentState == FishingState.Throwing)
            {
                var arcHeight = Mathf.Sin(t * Mathf.PI) * 2f;
                linePositions[i] = basePos + Vector3.up * arcHeight;
            }
            else
            {
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
        
        currentState = FishingState.None;
        GameManager.Instance.Player.AnchorPlayer(false);
        
        if (success)
        {
            GameManager.Instance.Player.Inventory.Add(currentFish, 1);
        }
    }

    public override void OnDown()
    {
        if (!canFish) return;

        switch (currentState)
        {
            case FishingState.None:
                currentState = FishingState.Prepared;
                animator.Play("Prepare", -1, 0f);
                GameManager.Instance.Player.AnchorPlayer(true);
                UIController.Instance.throwMinigame.StartMinigame(this);
                break;
            case FishingState.Waiting:
                if (!canCatch)
                {
                    EndFishing(false);
                    return;
                }

                currentState = FishingState.Fishing;
                UIController.Instance.fishingMinigame.StartMinigame(currentFish, throwStrength, this);
                
                break;
        }
    }

    public override void OnUp()
    {
        if (!canFish) return;

        switch (currentState)
        {
            case FishingState.Prepared:
                currentState = FishingState.Throwing;
                animator.Play("Throw", -1, 0f);
                throwStrength = UIController.Instance.throwMinigame.StopAndGetResults();
                targetPosition = GameManager.Instance.Player.transform.position + lastDirection * (5 * Mathf.Max(throwStrength, 0.2f));
                
                var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
                var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
                var gridPos = waterTilemap.WorldToCell(targetPosition);
                var waterTile = waterTilemap.GetTile(gridPos);
                if (!waterTile)
                {
                    EndFishing(false);
                    return;
                };
                
                currentFish = fishTable.GetRandomItem();
                currentWait = 0f;
                waitTime = Random.Range(2f, 10f);
                Debug.Log(waitTime);
                
                castProgress = 0f;
                lineRenderer.enabled = true;
                hook.position = targetPosition;
                hook.gameObject.SetActive(true);
                break;
        }
    }
    
    public override void UpdateDirection(Vector2 direction)
    {
        lastDirection = new Vector3(direction.x, direction.y, 0);
        var scale = gameObject.transform.localScale;
        scale.x = Mathf.Sign(direction.x);
        gameObject.transform.localScale = scale;
    }
}
