using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 MousePosition;
    public GameObject CardPrefab;
    public List<CardObject> Deck = new();
    public GameObject hand;
    public List<GameObject> handCards = new();
    public Slider SpiceSlider, SourSlider, SweetSlider, SavourySlider, SaltySlider;
    public int maxHandSize = 5;
    public int handSize = 0;
    public bool draggingCard;
    public GameObject draggedCard;
    public float cardGap;

    public int spicy;
    public int sweet;
    public int sour;
    public int savoury;
    public int salty;
    public bool mouse1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        MousePosition = Mouse.current.position.ReadValue();
        mouse1 = Input.GetMouseButton(0);
        foreach (GameObject card in handCards)
        {
            CardDrag drag = card.GetComponent<CardDrag>();
            drag.MouseState = mouse1;
            drag.MousePosition = MousePosition;
        }
        

        if (handSize < maxHandSize && Deck.Count > 0)
        {
            AddCardToHand(Deck[Random.Range(0, Deck.Count)]);
        }
        if(draggingCard)
        {
            pot.color = new Color (255,255,255,0.1f);
        }
        else
        {
            pot.color = Color.clear;
        }
    }


    public void RemoveCardFromHand(CardClass card)
    {
        // Removes a card from the hand (unfinished)
        handCards.Remove(card.gameObject);
        Destroy(card.gameObject);
        handSize--;
        Debug.Log("Removed a card from hand");
    }

    public void AddCardToHand(CardObject card)
    {
        // Adds a card to the hand
        GameObject cardObject = Instantiate(CardPrefab, hand.transform);
        CardDrag cardDrag = cardObject.GetComponent<CardDrag>();
        cardDrag.gameManager = this;
        cardDrag.menuScript = GameObject.Find("MenuManager").GetComponent<MenuScript>();
        handCards.Add(cardObject);
        
        cardDrag.defaultPosition = cardObject.transform.position;
        CardClass cardClass = cardObject.GetComponent<CardClass>();
        cardClass.SetFoodName(card.foodName);
        foreach (FlavourClass flavour in card.flavourClass)
        {
            cardClass.flavourClass.Add(flavour);
        }
        cardClass.InitializeCard();
        OrganiseHand();
        handSize++;
        
    }
    void OrganiseHand()
    {
        int currentCard = 1;
        foreach (GameObject card in handCards)
        {
            Vector3 newpos = new Vector3(currentCard switch 
            {
                1 => -cardGap * 2,
                2 => -cardGap,
                3 => 0f,
                4 => cardGap,
                5 => cardGap * 2,
                _ => 0f
            }, 0f, 0f);
            card.transform.localPosition = newpos;
            card.GetComponent<CardDrag>().defaultPosition = newpos;
            currentCard += 1;
        }
    }
    void AddFlavours(CardClass card)
    {

        foreach (FlavourClass flavourClass in card.flavourClass)
        {
            switch (flavourClass.Flavor)
            {
            case CardFlavor.Spicy:
                spicy = Mathf.Min(spicy + flavourClass.FlavourValue, 15);
                SpiceSlider.value = spicy;
                break;
            case CardFlavor.Sweet:
                sweet = Mathf.Min(sweet + flavourClass.FlavourValue, 15);
                SweetSlider.value = sweet;
                break;
            case CardFlavor.Sour:
                sour = Mathf.Min(sour + flavourClass.FlavourValue, 15);
                SourSlider.value = sour;
                break;
            case CardFlavor.Savoury:
                savoury = Mathf.Min(savoury + flavourClass.FlavourValue, 15);
                SavourySlider.value = savoury;
                break;
            case CardFlavor.Salty:
                salty = Mathf.Min(salty + flavourClass.FlavourValue, 15);
                SaltySlider.value = salty;
                break;
            default:
                Debug.LogWarning("Unknown flavor encountered: " + flavourClass.Flavor);
                break;
            }
        }
    }
    public void CardInteract(GameObject Interactor, GameObject Interractee)
    {
        if (Interactor == null || Interractee == null )
        {
            Debug.Log("Could not interract as one party is null");
            return;
        }
        CardClass interactingCard = Interactor.GetComponent<CardClass>();
        //Debug.Log("Card " + Interactor.GetComponent<CardClass>().foodName + " Interacted with " + Interractee.GetComponent<CardClass>().foodName);
        if (Interractee.CompareTag("Trash"))
        {
            Debug.Log("Trashing card...");
            RemoveCardFromHand(interactingCard);
            draggedCard = null;
            draggingCard = false;
        }
        else if (Interractee.CompareTag("Pot"))
        {
            AddFlavours(interactingCard);
            draggedCard = null;
            draggingCard = false;
            RemoveCardFromHand(interactingCard);
        }
        else if (Interractee.CompareTag("Card"))
        {
            draggedCard = null;
            draggingCard = false;
        }
    }
    public Image pot;

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