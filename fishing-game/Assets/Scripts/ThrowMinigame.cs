using System;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ThrowMinigame : MonoBehaviour
{
    private Vector2 targetPosition;
    private float timer;
    
    [Header("UI Elements")]
    public GameObject minigamePanel;
    public RectTransform catchArea;

    private bool released;
    private float progress;

    private void Start()
    {
        
    }

    public void StartMinigame()
    {
        minigamePanel.SetActive(true);

        released = false;
        progress = 0f;
        timer = 0f;
        
        StartCoroutine(RunMinigame());
    }
    
    private IEnumerator RunMinigame()
    {
        while (!released)
        {
            timer += Time.deltaTime;

            // var rect = catchArea.rect;
            // rect.size = new Vector2(150 * progress, 50);
            catchArea.sizeDelta = new Vector2(150 * progress, 50);
            catchArea.GetComponent<Image>().color = Color.HSVToRGB(0.33f * progress, 1, 1);

            progress = Mathf.Abs(Mathf.Sin(timer));
            
            yield return null;
        }
        
        minigamePanel.SetActive(false);
        GameManager.Instance.FishingController.EndThrow(progress);
    }

    public void Release()
    {
        released = true;
    }
}
