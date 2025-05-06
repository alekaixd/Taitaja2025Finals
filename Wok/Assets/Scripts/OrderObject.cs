using UnityEngine;

[CreateAssetMenu(fileName = "OrderObject", menuName = "OrderObjects/Order", order = 1)]
public class OrderObject : ScriptableObject
{
    public string orderDescription;
    public OrderRequirement orderRequirement = new();
}

[System.Serializable]
public class OrderRequirement
{
    public int spiceRequired;
    public int sweetnessRequired;
    public int sournessRequired;
    public int bitternessRequired;
    public int saltynessRequired;
}