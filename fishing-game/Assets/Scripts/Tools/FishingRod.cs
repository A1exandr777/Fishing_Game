using System;
using System.Linq;
using UnityEngine;
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
    
    public bool isFishing;
    public bool isThrowing;

    public bool canFish = true;
    public float sinceLastTimeFishing;
    public float delay = 2f;


    public override void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegments;
        linePositions = new Vector3[lineSegments];
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = lineMaterial;

        lineRenderer.enabled = false;
    }

    public void Update()
    {
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

    public void FinishedFishing()
    {
        canFish = false;
        
        lineRenderer.enabled = false;
    }

    public override void OnDown()
    {
        if (!canFish) return;
        if (isFishing || isThrowing || isCasting) return;
        
        animator.Play("Prepare", -1, 0f);

        lineRenderer.enabled = false;
        
        var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
        if (!waterTilemap) return;
            
        var worldPos = CameraController.Instance.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        var gridPos = waterTilemap.WorldToCell(worldPos);
        var waterTile = waterTilemap.GetTile(gridPos);

        targetPosition = new Vector3(worldPos.x, worldPos.y, hook.position.z);
        
        if (waterTile)
        {
            GameManager.Instance.FishingController.StartFishing(this);
        }
    }

    public override void OnUp()
    {
        if (!canFish) return;
        if (isFishing || isThrowing || isCasting) return;
        
        animator.Play("Throw", -1, 0f);
    
        // targetPosition = target;
        castProgress = 0f;
        isCasting = true;
        lineRenderer.enabled = true;
        hook.position = targetPosition;
        hook.gameObject.SetActive(true);
    }
    
    public override void UpdateDirection(Vector2 direction)
    {
        var scale = gameObject.transform.localScale;
        scale.x = Mathf.Sign(direction.x);
        gameObject.transform.localScale = scale;
    }
}
