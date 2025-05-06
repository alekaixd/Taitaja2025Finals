using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Remember to use the new Input System for mouse input
    public bool isHovered;
    public Vector2 DefaultPosition;
    public Vector2 DefaultScale;
    public Vector2 MousePosition;
    public GameManager gameManager;
    void Start()
    {
        
    }
        void Awake()
    {
        DefaultScale = transform.localScale;
    }
    void OnMouseEnter()
    {
        isHovered = true;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(new Vector3(1.1f, 1.1f,1f), 0.1f));
    }

    void OnMouseExit()
    {
        isHovered = false;
        StopAllCoroutines();
        StartCoroutine(ScaleTo(new Vector3(1f, 1f, 1f), 0.1f));
    }

    public IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition;

        if (targetScale.y > initialScale.y) // Scaling up
        {
            targetPosition.y += 50f;
        }
        else // Scaling down
        {
            targetPosition.y -= 50f;
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.localScale = new Vector3(
                Mathf.Lerp(initialScale.x, targetScale.x, elapsed / duration),
                Mathf.Lerp(initialScale.y, targetScale.y, elapsed / duration),
                1f);

            transform.position = new Vector3(
                Mathf.Lerp(initialPosition.x, targetPosition.x, elapsed / duration),
                Mathf.Lerp(initialPosition.y, targetPosition.y, elapsed / duration),
                initialPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.position = targetPosition;
    }

    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;
        DefaultPosition = transform.position;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the object stays in the same plane
            transform.position = mousePosition;
        }
        else
        {
            // Lerp back to the default position when not dragging
            transform.position = Vector3.Lerp(transform.position, DefaultPosition, Time.deltaTime * 10f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MousePosition = Input.mousePosition; // Use Input.mousePosition to get the correct screen-space mouse position
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        if (rectTransform != null)
        {
            Vector2 localMousePosition = rectTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(MousePosition));
            Rect rect = rectTransform.rect;

            bool isInside = rect.Contains(localMousePosition);
            //Debug.Log("Mouse Position: " + MousePosition + " Local Mouse Position: " + localMousePosition + " Rect: " + rect + " Is Inside: " + isInside);
            if (isInside && !isHovered)
            {
                OnMouseEnter();
            }
            else if (!isInside && isHovered)
            {
                OnMouseExit();
            }
        }
    }

}
