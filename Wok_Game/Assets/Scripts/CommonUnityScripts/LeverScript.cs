using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to a object that has a boxcollider2d
// deactivates another object when E is pressed

public class LeverScript : MonoBehaviour
{
    public GameObject activateObject;
    private bool hasContact;

    private void OnTriggerStay2D(Collider2D collision)
    {
        hasContact = true;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasContact = false;
    }

    private void Update()
    {
        if(hasContact == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && activateObject.activeSelf)
            {
                activateObject.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                activateObject.SetActive(true);
            }
        }
    }
}
