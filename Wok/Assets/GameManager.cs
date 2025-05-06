using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
