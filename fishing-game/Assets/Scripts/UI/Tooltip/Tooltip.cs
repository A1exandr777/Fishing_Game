using System;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI header;
    
    private void Update()
    {
        panel.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) + new Vector2(20, -20);
    }

    public void SetText(string text)
    {
        header.text = text;
    }
}
