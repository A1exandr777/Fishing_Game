using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public TextMeshProUGUI dialogueText, nameText;
    public Image portraitImage;

    public Button closeButton;

    private int dialogueIndex;
    private bool isTyping;

    public NPC currentNPC;

    public void Awake()
    {
        closeButton.onClick.AddListener(EndDialogue);
    }

    public void StartDialogue(NPC npc)
    {
        currentNPC = npc;
        
        currentNPC.isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(currentNPC.dialogueData.npcName);
        portraitImage.sprite = currentNPC.dialogueData.npcPortrait;

        gameObject.SetActive(true);

        StartCoroutine(TypeLine());
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(currentNPC.dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < currentNPC.dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");
        foreach (var letter in currentNPC.dialogueData.dialogueLines[dialogueIndex].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentNPC.dialogueData.typingSpeed);
        }
        isTyping = false;

        if (currentNPC.dialogueData.autoProgressLines.Length > dialogueIndex && currentNPC.dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(currentNPC.dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        currentNPC.isDialogueActive = false;
        dialogueText.SetText("");
        gameObject.SetActive(false);
    }
}
