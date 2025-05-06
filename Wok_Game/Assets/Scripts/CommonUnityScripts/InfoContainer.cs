using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoContainer : MonoBehaviour
{
    public int intVar;
    public float MasterVolume, MusicVolume;
    public string stringVar;
    public AudioClip audioVar;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Awake()
    {
        if (GameObject.Find(gameObject.name) && GameObject.Find(gameObject.name) != gameObject)
        {
            Destroy(gameObject);
        }
    }
}
