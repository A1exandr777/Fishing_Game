using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ThrowMinigame : MonoBehaviour
{
    [Header("Movement Settings")]
    public float circleRadius = 3f; // Radius of the circular boundary
    public float moveSpeed = 200f; // Speed of movement
    public float minWaitTime = 0.5f; // Minimum time between target changes
    public float maxWaitTime = 2f; // Maximum time between target changes

    private Vector2 targetPosition;
    private RectTransform rectTransform; // For UI elements
    private float timer;
    
    [Header("UI Elements")]
    public GameObject minigamePanel;
    public RectTransform catchArea;
    
    [Header("Minigame Settings")]
    public float baseCatchSpeed = 0.5f;
    public float difficultyMultiplier = 0.2f;
    
    private Item currentFish;
    private float catchProgress = 0f;
    private float timeLeft = 3f;
    private float fishPosition = 0.5f;
    private float fishMovementDirection = 1f;

    private void Start()
    {
        rectTransform = minigamePanel.GetComponent<RectTransform>();
    }

    public void StartMinigame()
    {
        minigamePanel.SetActive(true);
        
        catchProgress = 0f;
        timeLeft = 1f;
        fishPosition = 0.5f;
        
        StartCoroutine(RunMinigame());
    }
    
    private IEnumerator RunMinigame()
    {
        
        while (timeLeft > 0)
        {
            catchArea.anchoredPosition = Vector2.MoveTowards(
                catchArea.anchoredPosition, 
                targetPosition, 
                moveSpeed * Time.deltaTime
                );

            // Check if reached target
            var distanceToTarget = Vector2.Distance(catchArea.anchoredPosition, targetPosition);

            if (distanceToTarget < 0.01f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SetNewRandomTarget();
                }
            }
            
            // Debug.Log(timeLeft);
            // timeLeft -= Time.deltaTime;
            //
            // var horizontal = Input.GetAxisRaw("Horizontal");
            // var vertical = Input.GetAxisRaw("Vertical");

            // var deltaX = Mouse.current.delta.x.value * 100 * Time.deltaTime;
            // var deltaY = Mouse.current.delta.y.value * 100 * Time.deltaTime;
            // var xOffset = deltaX + Random.Range(-1000f, 1000f) * Time.deltaTime;
            // var yOffset = deltaY + Random.Range(-1000f, 1000f) * Time.deltaTime;
            // var pX = Mathf.Clamp(catchArea.anchoredPosition.x + xOffset, -100, 100);
            // var pY = Mathf.Clamp(catchArea.anchoredPosition.y + yOffset, -100, 100);
            //
            // catchArea.anchoredPosition = new Vector2(pX, pY);

            // var randomPos = RandomPointOnCircle() * 75;
            // catchArea.anchoredPosition = Vector2.MoveTowards(catchArea.anchoredPosition, randomPos, 1f);
            // catchArea.anchoredPosition += new Vector2(horizontal, vertical);
            
            yield return null;
        }
        
        minigamePanel.SetActive(false);
        GameManager.Instance.FishingController.EndThrow(0.5f);
    }
    
    void SetNewRandomTarget()
    {
        // Get a random point inside the circle
        // var randomDirection = Random.insideUnitCircle.normalized;
        // var randomDistance = Random.Range(0f, circleRadius);
        // targetPosition = randomDirection * randomDistance;
        targetPosition = RandomPointOnCircle() * 75;

        // Set new random wait time
        timer = Random.Range(minWaitTime, maxWaitTime);
    }

    private Vector2 RandomPointOnCircle()
    {
        var a = 2 * Mathf.PI * Random.value;
        var r = Mathf.Sqrt(Random.value);
        var x = r * Mathf.Cos(a);
        var y = r * Mathf.Sin(a);

        return new Vector2(x, y);
    }
}
