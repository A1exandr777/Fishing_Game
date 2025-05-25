using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialogue dialogueData;

    public int dialogueIndex;
    public bool isDialogueActive;

    public bool CanInteract()
    {
        return true;
        // return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null)
            return;
        if (isDialogueActive)
        {
            GameManager.Instance.DialogueController.NextLine();
            // NextLine();
        }
        else
        {
            GameManager.Instance.DialogueController.StartDialogue(this);
            // StartDialogue();
        }
    }

    // private void StartDialogue()
    // {
    //     isDialogueActive = true;
    //     dialogueIndex = 0;
    //
    //     nameText.SetText(dialogueData.npcName);
    //     portraitImage.sprite = dialogueData.npcPortrait;
    //
    //     dialoguePanel.SetActive(true);
    //
    //     StartCoroutine(TypeLine());
    // }
    //
    // private void NextLine()
    // {
    //     if (isTyping)
    //     {
    //         StopAllCoroutines();
    //         dialogueText.SetText(dialogueData.dialoguelines[dialogueIndex]);
    //         isTyping = false;
    //     }
    //     else if (++dialogueIndex < dialogueData.dialoguelines.Length)
    //     {
    //         StartCoroutine(TypeLine());
    //     }
    //     else
    //     {
    //         EndDialogue();
    //     }
    // }
    //
    // private IEnumerator TypeLine()
    // {
    //     isTyping = true;
    //     dialogueText.SetText("");
    //     foreach (var letter in dialogueData.dialoguelines[dialogueIndex].ToCharArray())
    //     {
    //         dialogueText.text += letter;
    //         yield return new WaitForSeconds(dialogueData.TypingSpeed);
    //     }
    //     isTyping = false;
    //
    //     if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
    //     {
    //         yield return new WaitForSeconds(dialogueData.autoProgressDelay);
    //         NextLine();
    //     }
    // }
    //
    // public void EndDialogue()
    // {
    //     StopAllCoroutines();
    //     isDialogueActive = false;
    //     dialogueText.SetText("");
    //     dialoguePanel.SetActive(false);
    // }
}
