using UnityEngine;
using System.Collections.Generic;

/// This class manages the deck of cards and handles the card drawing logic
/// needs to handle adding cards to the deck

public class DeckManager : MonoBehaviour
{
    /*
    public List<CardObject> UniqueCards = new(); //holds all cards in the game
    public List<CardObject> DefaultCards = new(); //default starting cards used to pick random cards from
    public List<CardObject> Deck = new(); //holds cards that are in the deck (default 15)

    public int[] uniqueCardIndex;

    public void AddCardsToDeck()
    {
        // Add 3 random cards from the default cards to the deck
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, DefaultCards.Count);
            Deck.Add(DefaultCards[randomIndex]);
        }
    }

    public void AddNewCardToDefault(int index)
    {
        int randomCardIndex = Random.Range(0, UniqueCards.Count);
        CardObject cardToAdd = UniqueCards[randomCardIndex];
        UniqueCards.RemoveAt(randomCardIndex);
        DefaultCards.Add(cardToAdd);
    }

    public void ShowNewCards()
    {
        // Ensure there are enough cards to pick from
        int cardsToPick = Mathf.Min(3, UniqueCards.Count);
        uniqueCardIndex = new int[cardsToPick];
        HashSet<int> selectedIndexes = new HashSet<int>();

        for (int i = 0; i < cardsToPick; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, UniqueCards.Count);
            } while (selectedIndexes.Contains(randomIndex)); // Ensure the index is unique

            selectedIndexes.Add(randomIndex);
            uniqueCardIndex[i] = randomIndex;
        }
    }
    */
}
