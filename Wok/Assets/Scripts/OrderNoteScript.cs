using UnityEngine;
using TMPro;

public class OrderNoteScript : MonoBehaviour
{
    public TextMeshProUGUI titleText; 

    public TextMeshProUGUI descriptionText;
    public void SetInfoText(string title, string desc)
    {
        titleText.text = title;
        descriptionText.text = desc;
    }
}
