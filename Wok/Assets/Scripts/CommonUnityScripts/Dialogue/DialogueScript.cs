using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// assign dialogue text and dialogue window
/// 
/// get dialogue gets the next dialogue line from the scriptable object
/// scriptable object is made in the scriptable object folder
/// </summary>

public class DialogueScript : MonoBehaviour
{
    public GameObject dialogueWindow;
    public TextMeshProUGUI dialogueText;
    public DialogueScriptableObject dialogueObject;
    private bool isDialogueActive = false;
    private bool isSetComplete = false;

    public void GetDialogue()
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
            dialogueWindow.SetActive(true); // Open the dialogue window when starting a new set
            dialogueObject.setDialogueSet(0); // Always start at index 0
        }

        if (isSetComplete)
        {
            if (dialogueObject.moveToNextSet())
            {
                isSetComplete = false;
                dialogueWindow.SetActive(true); // Re-enable the dialogue window for the next set
            }
            else
            {
                dialogueObject.resetDialogue();
                isDialogueActive = false;
                return;
            }
        }

        string dialogueLine = dialogueObject.returnDialogue();
        if (dialogueLine == null)
        {
            dialogueWindow.SetActive(false); // Disable the dialogue window when the set is done
            isSetComplete = true;
            return;
        }

        dialogueText.text = dialogueLine;
    }
}
