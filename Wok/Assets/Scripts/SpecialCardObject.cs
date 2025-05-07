using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "SpecialCard", menuName = "SpecialCardObjects/SpecialCard", order = 1)]
public class SpecialCardObject : ScriptableObject
{
    public string foodName;
    public List<FlavourClass> flavourClass = new();
}
