using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 MousePosition;
    public GameObject CardPrefab;
    public List<CardObject> Deck = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void AddCardToDeck()
    {
        // Adds a card to the deck
    }

    public void addCardToHand(CardObject card)
    {
        // Adds a card to the hand
        GameObject cardObject = Instantiate(CardPrefab, transform);
        CardClass cardClass = cardObject.GetComponent<CardClass>();
        cardClass.SetFoodName(card.foodName);
        foreach (FlavourClass flavour in card.flavourClass)
        {
            cardClass.flavourClass.Add(flavour);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MousePosition = Mouse.current.position.ReadValue();
    }
}
    public enum CardFlavor
    {
        None = 0,
        Spicy = 1,
        Sweet = 2,
        Sour = 3,
        Savoury = 4,
        Salty = 5,
    }

[System.Serializable]
public class FlavourClass
{
    public CardFlavor Flavor;
    public int FlavourValue;
    public FlavourClass(CardFlavor flavor, int flavourValue)
    {
        Flavor = flavor;
        FlavourValue = flavourValue;
    }

    public int GetFlavourIndex()
    {
        return (int)Flavor;
    }
}

    public class FlavourBuffClass
    {
        CardFlavor Flavor;
        int FlavourValue;
        int RoundDuration;
        public FlavourBuffClass(CardFlavor flavor, int flavourValue, int roundDuration)
        {
            Flavor = flavor;
            FlavourValue = flavourValue;
            RoundDuration = roundDuration;
        }
    }