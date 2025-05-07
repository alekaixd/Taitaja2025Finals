using UnityEngine;

public class PotScript : MonoBehaviour
{
    public bool isHovered;
    public Vector2 MousePosition;
    public GameManager gameManager;
    public MenuScript menuScript;

    void Start()
    {
        
    }
    void Awake()
    {
        menuScript = FindFirstObjectByType<MenuScript>();
        gameManager = FindFirstObjectByType<GameManager>();
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

    // Update is called once per frame
    public  float hoverTime = 0f; // Tracks how long the mouse has been hovering

    void FixedUpdate()
    {
        MousePosition = Input.mousePosition; // Use Input.mousePosition to get the correct screen-space mouse position
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            Vector2 localMousePosition = rectTransform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(MousePosition));
            Rect rect = rectTransform.rect;

            bool isInside = rect.Contains(localMousePosition);

            if (isInside && !isHovered)
            {
                OnMouseEnter();
            }
            else if (!isInside && isHovered)
            {
                OnMouseExit();
                hoverTime = 0f; // Reset hover time when exiting
            }
        }

        if (isHovered && gameManager.draggingCard)
        {
            hoverTime += Time.fixedDeltaTime; // Increment hover time while hovering
            if (hoverTime >= 0.25f)
            {
                Debug.Log("Wok Interact");
                gameManager.CardInteract(gameManager.draggedCard, gameObject);
                hoverTime = 0f; // Reset hover time after interaction
            }
        }
        else
        {
            hoverTime = 0f; // Reset hover time if not hovering or not dragging
        }
    }

}
