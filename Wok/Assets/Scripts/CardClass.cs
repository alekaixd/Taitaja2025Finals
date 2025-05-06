using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardClass : MonoBehaviour
{
    public TextMeshProUGUI foodNameText;
    public string foodName;
    public List<FlavourClass> flavourClass = new();
    public GameObject flavourTextPrefab;
    public GameObject flavourTextParent;
    public Image cardImage;
    public List<Sprite> cardIcons;
    public List<Sprite> cardBackgrounds;

    public void SetFoodName(string name)
    {
        foodName = name;
        foodNameText.text = name;
    }

    public void AddFlavour(FlavourClass flavour)
    {
        flavourClass.Add(flavour);
    }

    public void Awake()
    {
        foodNameText.text = foodName;
        foreach (FlavourClass flavour in flavourClass)
        {
            GameObject flavourText = Instantiate(flavourTextPrefab, flavourTextParent.transform);
            TextMeshProUGUI text = flavourText.GetComponent<TextMeshProUGUI>();
            text.text = $"+ {flavour.FlavourValue} {flavour.Flavor}";
        }
        
        cardImage.sprite = cardIcons[flavourClass[0].GetFlavourIndex() - 1];
        gameObject.GetComponent<Image>().sprite = cardBackgrounds[flavourClass[0].GetFlavourIndex() - 1];
    }
}
