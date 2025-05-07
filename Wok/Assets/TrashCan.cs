using System.Collections;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public bool isHovered;
    public Vector2 defaultPosition;
    public Vector3 DefaultScale;
    public Vector2 MousePosition;
    public GameManager gameManager;
    public MenuScript menuScript;
    public float PopupSpeed = 0.1f;
    public float PopupHeight = 50f; // Height to pop up when scaling
    public float scalespeed = 0.1f; // Speed of scaling
    public bool isDragged = false; // Flag to check if the card is being dragged
    public Vector3 scaleSize; // Size to scale to
    public float HoverDelay = 0.1f; // Delay before the card pops up

    void Start()
    {
        
    }
    void Awake()
    {
        menuScript = FindFirstObjectByType<MenuScript>();
        gameManager = FindFirstObjectByType<GameManager>();
        DefaultScale = Vector3.one;
    }
    void OnMouseEnter()
    {
        if (!isHovered) // Ensure it only moves up once
        {
            isHovered = true;
        }
    }

    void OnMouseExit()
    {
        if (isHovered) // Ensure it only moves down once
        {
            isHovered = false;
        }
    }

    void OnMouseDown()
    {
        if (isHovered && gameManager.draggingCard == false) // Only allow dragging if hovered
        {
            gameManager.draggedCard = gameObject;
            isDragged = true;
            gameManager.draggingCard = true;
        }
    }

    void OnMouseUp()
    {
        if (isHovered && gameManager.draggingCard && gameManager.draggedCard != null)
        {
            Debug.Log("Trash Interract");
            gameManager.CardInteract(gameManager.draggedCard, gameObject);
        }
        isDragged = false;
        gameManager.draggingCard = false;
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
        if (Input.GetMouseButton(0))
        {
            OnMouseDown();
        }
        else if (!Input.GetMouseButton(0))
        {
            OnMouseUp();
        }
    }

}
