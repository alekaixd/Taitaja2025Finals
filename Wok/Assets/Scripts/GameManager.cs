using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector2 MousePosition;
    public GameObject CardPrefab;
    public GameObject SpecialCardPrefab;
    public List<CardObject> GameDeck = new();
    public List<SpecialCardObject> SpecialDeck = new();
    public GameObject hand;
    public GameObject specialHand;
    public OrderManager orderManager;
    public List<GameObject> handCards = new();
    public List<GameObject> specialHandCards = new();
    public Slider SpiceSlider, SourSlider, SweetSlider, SavourySlider, SaltySlider;
    public int maxHandSize = 5;
    public int handSize = 0;
    public bool draggingCard;
    public GameObject draggedCard;
    public float cardGap;
    public TMP_Text Decknumber;
    public int Decksize = 15;

    public int spicy;
    public int sweet;
    public int sour;
    public int savoury;
    public int salty;
    public bool mouse1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; i < 3; i++) { 
            AddSpecialCard(SpecialDeck[Random.Range(0, SpecialDeck.Count)]);
        }
    }
    public void ResetFlavours()
    {
        spicy = 0;
        sweet = 0;
        sour = 0;
        savoury = 0;
        salty = 0;

        SpiceSlider.value = spicy;
        SweetSlider.value = sweet;
        SourSlider.value = sour;
        SavourySlider.value = savoury;
        SaltySlider.value = salty;

        Decksize = 15;
        Decknumber.text = Decksize.ToString();
        Debug.Log("All flavours have been reset to 0.");
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


        if (handSize < maxHandSize && Decksize > 0)
        {
            AddCardToHand(GameDeck[Random.Range(0, GameDeck.Count)]);
        }
        if (draggingCard)
        {
            pot.color = new Color(255, 255, 255, 0.1f);
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
        if (Decksize > 0)
        {
            Decksize--;
        }

        Destroy(card.gameObject);
        handSize--;
        if (handSize == 0)
        {
            orderManager.GameOver("Ran out of Cards");
        }
        Decknumber.text = Decksize.ToString();
        Debug.Log("Removed a card from hand");
    }

    public void AddCardToHand(CardObject card)
    {
        Decknumber.text = Decksize.ToString();
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

    public void AddSpecialCard(SpecialCardObject sCard)
    {
        // spawn 3 special cards
        // edit default positions on card drag script
        // set food names and flavours

        GameObject cardObject = Instantiate(SpecialCardPrefab, specialHand.transform);
        CardDrag cardDrag = cardObject.GetComponent<CardDrag>();
        cardDrag.gameManager = this;
        cardDrag.menuScript = GameObject.Find("MenuManager").GetComponent<MenuScript>();
        specialHandCards.Add(cardObject);

        cardDrag.defaultPosition = cardObject.transform.position;
        InteractableCardClass iCardClass = cardObject.GetComponent<InteractableCardClass>();
        iCardClass.SetFoodName(sCard.foodName);
        iCardClass.flavour = sCard.flavourClass;
        iCardClass.InitializeCard();
        OrganizeSpecialHand();
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

    public void OrganizeSpecialHand()
    {
        int currentCard = 1;
        foreach (GameObject card in specialHandCards)
        {
            Vector3 newpos = new Vector3(currentCard switch
            {
                1 => -cardGap,
                2 => 0f,
                3 => cardGap,
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
        if (Interactor == null || Interractee == null)
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
            orderManager.IncrementIngridients();
            draggedCard = null;
            draggingCard = false;
            RemoveCardFromHand(interactingCard);
        }
        else if (Interractee.CompareTag("Card") || Interractee.CompareTag("S-Card"))
        {
            Debug.Log("Card Interract");
            if (Interactor.CompareTag("S-Card"))
            {

            }
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

public class SpecialFlavourClass
{
    public CardFlavor Flavor;
    public int FlavourValue;
    public string prefix;
    public SpecialFlavourClass(CardFlavor flavor, int flavourValue, int roundDuration)
    {
        Flavor = flavor;
        FlavourValue = flavourValue;
        prefix = "";
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