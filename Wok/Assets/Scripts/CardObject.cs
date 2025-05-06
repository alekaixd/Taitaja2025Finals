using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardObjects/Card", order = 1)]
public class CardObject : ScriptableObject
{
    public string foodName;
    public List<FlavourClass> flavourClass = new();
}
