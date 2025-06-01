using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class FishingMinigame : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject minigamePanel;
    public Image progressBar;
    public Image fishIcon;
    public RectTransform catchArea;
    public RectTransform fishIndicator;
    
    [Header("Minigame Settings")]
    public float baseCatchSpeed = 0.5f;
    public float difficultyMultiplier = 0.2f;
    
    private Item currentFish;
    private float catchProgress = 0f;
    private bool isReeling;
    private float fishPosition = 0.5f;
    private float fishMovementDirection = 1f;

    private bool started;

    public FishingRod currentRod;
    
    public void StartMinigame(Item fish, float strength, FishingRod rod)
    {
        currentRod = rod;

        started = false;
        
        currentFish = fish;
        minigamePanel.SetActive(true);
        
        fishIcon.sprite = fish.Icon;
        catchProgress = 0.5f;
        fishPosition = 0.0f;
        
        StartCoroutine(RunMinigame());
    }
    
    private IEnumerator RunMinigame()
    {
        while (catchProgress is < 1f and > 0f)
        {
            var progressRect = progressBar.rectTransform.rect;
            progressBar.rectTransform.sizeDelta = new Vector2(
                progressRect.size.x,
                catchProgress * minigamePanel.transform.GetComponent<RectTransform>().rect.size.y
            );
            fishIndicator.anchoredPosition = new Vector2(
                fishIndicator.anchoredPosition.x,
                Mathf.Lerp(0, minigamePanel.transform.GetComponent<RectTransform>().rect.size.y - fishIndicator.rect.size.y, fishPosition)
            );
            
            if (!started)
            {
                catchArea.anchoredPosition = new Vector2(catchArea.anchoredPosition.x, 0);
                
                yield return null;
                continue;
            };
            
            UpdateFishPosition();
            
            if (isReeling)
            {
                // catchArea.position += new Vector3(0, 150f * Time.deltaTime);
                catchArea.anchoredPosition = new Vector2(
                    catchArea.anchoredPosition.x,
                    Mathf.Clamp(
                        catchArea.anchoredPosition.y + 150f * Time.deltaTime,
                        0, minigamePanel.transform.GetComponent<RectTransform>().rect.size.y - catchArea.rect.size.y
                    )
                );
                // var difficultyLevel = 1; // currentFish.difficultyLevel
                // float catchSpeed = baseCatchSpeed - (difficultyLevel * difficultyMultiplier);
                // catchProgress += catchSpeed * Time.deltaTime;
                //
                // // Check if fish is in catch area
                // float distanceToFish = Mathf.Abs(fishPosition - 0.5f);
                // if(distanceToFish > 0.3f) // Catch area size
                // {
                //     catchProgress -= Time.deltaTime * 0.5f; // Penalty for missing
                // }
            }
            else
            {
                // catchArea.position -= new Vector3(0, 300f * Time.deltaTime);
                catchArea.anchoredPosition = new Vector2(
                    catchArea.anchoredPosition.x,
                    Mathf.Clamp(
                        catchArea.anchoredPosition.y - 300f * Time.deltaTime,
                        0, minigamePanel.transform.GetComponent<RectTransform>().rect.size.y - catchArea.rect.size.y
                        )
                    );
            }
            
            var indPosY = fishIndicator.position.y;
            var areaPosY = catchArea.position.y;
            if (indPosY >= areaPosY && indPosY + fishIndicator.rect.size.y <= areaPosY + catchArea.rect.size.y)
            {
                catchProgress += 2 * baseCatchSpeed * Time.deltaTime;
            }
            else
            {
                catchProgress -= Time.deltaTime * 0.2f;
            }
            
            catchProgress = Mathf.Clamp01(catchProgress);
            
            // var progressRect = progressBar.rectTransform.rect;
            // progressBar.rectTransform.sizeDelta = new Vector2(
            //     progressRect.size.x,
            //     catchProgress * minigamePanel.transform.GetComponent<RectTransform>().rect.size.y
            //     );
            // fishIndicator.anchoredPosition = new Vector2(
            //     fishIndicator.anchoredPosition.x,
            //     Mathf.Lerp(0, minigamePanel.transform.GetComponent<RectTransform>().rect.size.y - fishIndicator.rect.size.y, fishPosition)
            // );
            
            yield return null;
        }
        
        var success = catchProgress >= 1f;
        minigamePanel.SetActive(false);
        // GameManager.Instance.FishingController.EndFishing(success);
        currentRod.EndFishing(success);
    }
    
    private void UpdateFishPosition()
    {
        // Fish moves based on its pattern
        var movementPatternIntensity = 0.1f; // currentFish.movementPatternIntensity
        var escapeSpeed = 0.1f; // currentFish.escapeSpeed
        var movementFactor = movementPatternIntensity;
        fishPosition += Random.Range(-0.01f, 0.01f) * movementFactor;
        fishPosition += 0.01f * fishMovementDirection * escapeSpeed;
        
        // Change direction randomly
        if (Random.value < 0.02f)
        {
            fishMovementDirection *= -1;
        }
        
        // Clamp position
        fishPosition = Mathf.Clamp01(fishPosition);
    }
    
    public void StartReeling()
    {
        // Debug.Log(1);
        started = true;
        isReeling = true;
    }
    
    public void StopReeling()
    {
        isReeling = false;
    }
}
