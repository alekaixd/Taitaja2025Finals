using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach to a bullet prefab
// makes a bullet start flying forwards and destroys it after a while

public class BulletScript : MonoBehaviour
{
    private float bulletSpeed = 15.0f;
    

    void Start()
    {
        StartCoroutine(BulletLifetime());
    }
    
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime);
        
    }



    private IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
