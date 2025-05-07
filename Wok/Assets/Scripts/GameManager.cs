using System.Collections.Generic;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    public void CardInteract(GameObject Interactor, GameObject Interractee)
    {
        if (Interactor == null || Interractee == null )
        {
            Debug.Log("Could not interract as one party is null");
            return;
        }
        //Debug.Log("Card " + Interactor.GetComponent<CardClass>().foodName + " Interacted with " + Interractee.GetComponent<CardClass>().foodName);
        if (Interractee.CompareTag("Trash"))
        {
            Debug.Log("Trashing card...");
            RemoveCardFromHand(Interactor.GetComponent<CardClass>());
            draggedCard = null;
            draggingCard = false;
        }
        if (Interractee.CompareTag("Pot"))
        {
            RemoveCardFromHand(Interactor.GetComponent<CardClass>());
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MousePosition = Mouse.current.position.ReadValue();

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