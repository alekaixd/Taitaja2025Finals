using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue", order = 1)]
public class DialogueScriptableObject : ScriptableObject
{
    public List<DialogueSet> dialogueSets;
    private int dialoguePosition = 0;
    private int currentSetIndex = 0;

    public string returnDialogue()
    {
        if (currentSetIndex >= dialogueSets.Count)
        {
            Debug.Log("No more dialogue sets available.");
            return null;
        }

        DialogueSet currentSet = dialogueSets[currentSetIndex];
        Debug.Log(currentSet.dialogues.Length + " pos: " + dialoguePosition);

        if (dialoguePosition >= currentSet.dialogues.Length)
        {
            Debug.Log("Returning null");
            return null;
        }

        string returningDialogue = currentSet.dialogues[dialoguePosition];
        dialoguePosition++;
        return returningDialogue;
    }

    public void resetDialogue()
    {
        dialoguePosition = 0;
    }

    public void setDialogueSet(int setIndex)
    {
        if (setIndex >= 0 && setIndex < dialogueSets.Count)
        {
            currentSetIndex = setIndex;
            resetDialogue();
        }
        else
        {
            Debug.LogError("Invalid dialogue set index.");
        }
    }

    public bool moveToNextSet()
    {
        if (currentSetIndex + 1 < dialogueSets.Count)
        {
            currentSetIndex++;
            resetDialogue();
            return true;
        }
        return false;
    }
}
