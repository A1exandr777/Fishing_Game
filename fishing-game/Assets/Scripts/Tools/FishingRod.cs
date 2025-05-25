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


    public override void Init()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = lineSegments;
        linePositions = new Vector3[lineSegments];
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = lineMaterial;
    }

    public void Update()
    {
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

    public override void OnDown()
    {
        animator.Play("Prepare", -1, 0f);
        
        var allTilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
        var waterTilemap = allTilemaps.FirstOrDefault(tilemap => tilemap.name == "Water");
        if (!waterTilemap) return;
            
        var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var gridPos = waterTilemap.WorldToCell(worldPos);
        var waterTile = waterTilemap.GetTile(gridPos);

        targetPosition = worldPos;
        
        if (waterTile)
        {
            GameManager.Instance.FishingController.StartFishing();
        }
    }

    public override void OnUp()
    {
        animator.Play("Throw", -1, 0f);
        
        if (isCasting) return;
    
        // targetPosition = target;
        castProgress = 0f;
        isCasting = true;
        hook.position = targetPosition;
        Debug.Log(targetPosition);
        hook.gameObject.SetActive(true);
    }
    
    public override void UpdateDirection(Vector2 direction)
    {
        var scale = gameObject.transform.localScale;
        scale.x = Mathf.Sign(direction.x);
        gameObject.transform.localScale = scale;
    }

    public void Prepare()
    {
        animator.Play("Prepare", -1, 0f);
        // animator.Play("Ready", -1, 0f);
    }

    public void Throw()
    {
        animator.Play("Throw", -1, 0f);
        // animator.Play("Idle", -1, 0f);
    }
}
