using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Remember to use the new Input System for mouse input
    public bool isHovered;
    public Vector2 defaultPosition;
    public Vector2 DefaultScale;
    public Vector2 MousePosition;
    public GameManager gameManager;     public MenuScript menuScript;
    public float PopupSpeed = 0.1f;
    public float PopupHeight = 50f; // Height to pop up when scaling
    public float scalespeed = 0.1f; // Speed of scaling
    public bool isDragged = false; // Flag to check if the card is being dragged
    public Vector3 scaleSize = new Vector3(1.1f,1.1f,1.1f); // Size to scale to
    void Start()
    {
        
    }
    void Awake()
    {
        menuScript = FindFirstObjectByType<MenuScript>();
        gameManager = FindFirstObjectByType<GameManager>();
        DefaultScale = transform.localScale;
    }
    void OnMouseEnter()
    {
        if (!isHovered) // Ensure it only moves up once
        {
            isHovered = true;
            StopAllCoroutines();
            StartCoroutine(ScaleTo(scaleSize, scalespeed, PopupSpeed, true));
        }
    }

    void OnMouseExit()
    {
        if (isHovered) // Ensure it only moves down once
        {
            isHovered = false;
            StopAllCoroutines();
            StartCoroutine(ScaleTo(scaleSize, scalespeed, PopupSpeed, false));
        }
    }

    public IEnumerator ScaleTo(Vector3 targetScale, float expandspeed, float PopupSpeed, bool moveUp)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = moveUp ? new Vector3(initialPosition.x, defaultPosition.y + PopupHeight, initialPosition.z) : new Vector3(initialPosition.x, defaultPosition.y, initialPosition.z);

        float elapsed = 0f;

        while (elapsed < expandspeed)
        {
            transform.localScale = new Vector3(
                Mathf.Lerp(initialScale.x, targetScale.x, elapsed / expandspeed),
                Mathf.Lerp(initialScale.y, targetScale.y, elapsed / expandspeed),
                1f);

            if (moveUp || (!moveUp && isHovered == false)) // Only move down if unhovering
            {
                transform.position = new Vector3(
                    Mathf.Lerp(initialPosition.x, targetPosition.x, elapsed / PopupSpeed),
                    Mathf.Lerp(initialPosition.y, targetPosition.y, elapsed / PopupSpeed),
                    initialPosition.z);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        if (moveUp || (!moveUp && isHovered == false)) // Finalize position only if unhovering
        {
            transform.position = targetPosition;
        }
    }

    void OnMouseDown()
    {
        if (isHovered) // Only allow dragging if hovered
        {
            isDragged = true;
        }
    }

    void OnMouseUp()
    {
        isDragged = false;
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
        if (menuScript.MouseState.started)
        {
            OnMouseDown();
        }
        else if (menuScript.MouseState.canceled)
        {
            OnMouseUp();
        }
        if (isDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the object stays in the same plane
            transform.position = mousePosition;
        }
        else if (!isHovered) // Only lerp back to default position if not hovered
        {
            // Lerp back to the default position when not dragging and not hovered
            transform.position = Vector3.Lerp(transform.position, defaultPosition, PopupSpeed);
        }
    }

}
