using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "OrderObject", menuName = "OrderObjects/Order", order = 1)]
public class OrderObject : ScriptableObject
{
    public string orderName;
    public string orderDescription;
    public List<OrderRequirement> orderRequirement = new();
}

[System.Serializable]
public class OrderRequirement
{
    public CardFlavor flavor;
    public int flavourValue;
    public bool isReversedRequirement; // If true, the requirement is reversed, meaning the player must not have this flavour in their order
}