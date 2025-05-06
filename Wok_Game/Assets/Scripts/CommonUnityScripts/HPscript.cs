using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// assign to slider object
/// change hp by calling targetHP = newHP
/// </summary>

public class HPscript : MonoBehaviour
{
    public float targetHP;
    public float currentHP;
    public float lerpSpeed = 5;

    public void Awake()
    {
        currentHP = 100f;
        gameObject.GetComponent<Slider>().value = currentHP;
        targetHP = currentHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(currentHP - targetHP) > 0.01f)
        {
            currentHP = Mathf.MoveTowards(currentHP, targetHP, lerpSpeed * Time.deltaTime);
            gameObject.GetComponent<Slider>().value = currentHP;
        }
    }
}
