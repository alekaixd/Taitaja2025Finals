using System.Collections.Generic;
using UnityEngine;

public class CardClass : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string CardName;
    public string CardDescription;
    public CardFlavor Flavor;
    public CardFlavor SecondaryFlavor;
    public List<CardFlavor> FlavorList = new(); // Additional flavors
}
