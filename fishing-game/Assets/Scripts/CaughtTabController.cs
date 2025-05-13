using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaughtTabController : MonoBehaviour
{
    public int perPage = 24;
    public GameObject fishContainer;
    public TextMeshProUGUI pageNumber;
    public Button nextButton;
    public Button previousButton;

    private List<GameObject> fishIcons = new();
    private int currentPage = 1;

    private int totalFishCount;
    private int maxPage;

    private List<Item> allFish;

    public GameObject fishIconPrefab;

    private void Start()
    {
        totalFishCount = ItemDatabase.GetAllFish().Count;
        maxPage = Mathf.CeilToInt(totalFishCount / (float)perPage);

        allFish = ItemDatabase.GetAllFish();
        
        foreach (var fish in allFish)
        {
            var icon = Instantiate(fishIconPrefab, fishContainer.transform);
            icon.GetComponent<Image>().sprite = fish.Icon;
            icon.SetActive(false);
            fishIcons.Add(icon);
        }
        
        UpdateCaught();
        
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);

        Events.ItemAdded += UpdateCaught;
    }

    private void UpdateCaught()
    {
        pageNumber.text = currentPage.ToString();
        
        var caughtFish = GameManager.Instance.Player.caughtFish;

        foreach (var icon in fishIcons)
        {
            icon.SetActive(false);
        }

        var from = perPage * (currentPage - 1);
        var to = Mathf.Clamp(from + perPage, from, totalFishCount);
        for (var i = from; i < to; i++)
        {
            var hasCaught = caughtFish.TryGetValue(allFish[i].Name, out var catchCount) && catchCount != 0;

            var icon = fishIcons[i];
            var image = icon.GetComponent<Image>();
            icon.SetActive(true);
            if (!hasCaught)
            {
                image.color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                image.color = Color.white;
            }
        }
    }

    private void NextPage()
    {
        currentPage = Mathf.Clamp(currentPage + 1, 1, maxPage);
        UpdateCaught();
    }
    
    private void PreviousPage()
    {
        currentPage = Mathf.Clamp(currentPage - 1, 1, maxPage);
        UpdateCaught();
    }
}
