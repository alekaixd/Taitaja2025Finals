using UnityEngine;
using TMPro;

public class InteractableCardClass : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public GameObject flavourTextPrefab;
    public GameObject flavourTextParent;
    public FlavourClass flavour;

    string foodname;

    public void SetFoodName(string name)
    {
        foodname = name;
        titleText.text = name;
    }

    public void AddNewFlavour(FlavourClass addFlavour)
    {
        flavour = addFlavour;
    }

    public void InitializeCard()
    {
        GameObject flavourText = Instantiate(flavourTextPrefab, flavourTextParent.transform);
        TextMeshProUGUI text = flavourText.GetComponent<TextMeshProUGUI>();
        text.text = $"+ {flavour.FlavourValue} {flavour.Flavor}";
    }
}
