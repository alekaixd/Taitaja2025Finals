using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 MousePosition;
    public GameObject CardPrefab;
    public List<CardClass> Deck = new();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    void AddCardToDeck()
    {
        // Adds a card to the deck
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MousePosition = Mouse.current.position.ReadValue();
    }
}
    public enum CardFlavor
    {
        Spicy = 0,
        Sweet = 1,
        Sour = 2,
        Savoury = 3,
        Salty = 4,
    }

    public class FlavourClass
    {
        CardFlavor Flavor;
        int FlavourValue;
        public FlavourClass(CardFlavor flavor, int flavourValue)
        {
            Flavor = flavor;
            FlavourValue = flavourValue;
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