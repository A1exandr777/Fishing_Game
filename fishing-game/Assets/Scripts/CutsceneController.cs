using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject cutsceneUI;
    public TextMeshProUGUI textUI;

    public float textFadeTime = 0.5f;
    public float fadeTime = 0.5f;
    
    public void PlayCutscene(Cutscene cutscene, bool fadeOut = true, Action callback = null)
    {
        StartCoroutine(ShowCutscene(cutscene, fadeOut, callback));
    }
    
    private IEnumerator ShowCutscene(Cutscene cutscene, bool fadeOut = true, Action callback = null)
    {
        cutsceneUI.SetActive(true);
        
        var currentLine = 0;
        var textLines = cutscene.textLines;
        
        var timer = 0f;
        
        // Fade in
        while (timer < fadeTime)
        {
            SetAlpha(Mathf.Lerp(0f, 1f, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);
        
        while (currentLine < textLines.Length)
        {
            var text = textLines[currentLine];
            yield return StartCoroutine(FadeText(text, 0f, 1f));
            
            yield return new WaitForSeconds(text.Length / 20f);
            
            yield return StartCoroutine(FadeText(text, 1f, 0f));
            
            currentLine++;
        }
        
        // Fade out
        if (fadeOut)
        {
            timer = 0f;
            while (timer < fadeTime)
            {
                SetAlpha(Mathf.Lerp(1f, 0f, timer / fadeTime));
                timer += Time.deltaTime;
                yield return null;
            }
            SetAlpha(0f);
        }
        
        cutsceneUI.SetActive(false);

        callback?.Invoke();

        // Cutscene ended - you could load next scene here
        // SceneManager.LoadScene("NextSceneName");
    }
    
    private void SetAlpha(float alpha)
    {
        var currentColor = GameManager.Instance.UIController.fade.color;
        currentColor.a = Mathf.Clamp(alpha, 0f, 1f);
        GameManager.Instance.UIController.fade.color = currentColor;
    }
    
    private IEnumerator FadeText(string text, float startAlpha, float endAlpha)
    {
        textUI.gameObject.SetActive(true);
        textUI.text = text;
        var color = textUI.color;
        
        var elapsedTime = 0f;
        
        while (elapsedTime < textFadeTime)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / textFadeTime);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            textUI.color = color;
            yield return null;
        }
        
        color.a = endAlpha;
        textUI.color = color;
        
        if (endAlpha == 0f)
        {
            textUI.gameObject.SetActive(false);
        }
    }
}
