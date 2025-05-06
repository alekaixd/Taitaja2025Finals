using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;

    public Transform GetDestination(){
        UnityEngine.Debug.Log("Destinaatio löytynyt");
        return destination;
    }
}
