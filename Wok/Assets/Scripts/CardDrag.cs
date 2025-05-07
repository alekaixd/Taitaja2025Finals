using System.Collections;
using UnityEngine;

public class CardDrag : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Remember to use the new Input System for mouse input
    public bool isHovered;
    public Vector2 defaultPosition;
    public bool MouseState;
    public Vector3 DefaultScale;
    public Vector2 MousePosition;
    public GameManager gameManager;
    public MenuScript menuScript;
    public MusicScript musicScript;
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
            StopAllCoroutines();
            StartCoroutine(HoverWithDelay());
        }
    }

    IEnumerator HoverWithDelay()
    {
        yield return new WaitForSeconds(HoverDelay); // Add delay before scaling
        StartCoroutine(ScaleTo(scaleSize, scalespeed * 2, PopupSpeed * 2, true)); // Make scaling slower
    }

    void OnMouseExit()
    {
        if (isHovered) // Ensure it only moves down once
        {
            isHovered = false;
            StopAllCoroutines();
            StartCoroutine(ScaleTo(scaleSize, scalespeed * 2, PopupSpeed * 2, false)); // Make scaling slower
        }
    }

    public IEnumerator ScaleTo(Vector3 targetScale, float expandspeed, float PopupSpeed, bool moveUp)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 initialPosition = transform.localPosition;
        Vector3 targetPosition = moveUp ? new Vector3(initialPosition.x, defaultPosition.y + PopupHeight, initialPosition.z) : new Vector3(initialPosition.x, defaultPosition.y, initialPosition.z);
        if (returningToDefaultPos)
        {
            yield return null;
        }
        float elapsed = 0f;

        while (elapsed < 1)
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
                    1f);
            }

            elapsed += Time.deltaTime;
            
        }
        yield return null;

        transform.localScale = targetScale;
        if (moveUp || (!moveUp && isHovered == false)) // Finalize position only if unhovering
        {
            transform.localPosition = targetPosition;
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
        if (gameManager.draggedCard != gameObject && isHovered && gameManager.draggingCard)
        {
            gameManager.CardInteract(gameManager.draggedCard, gameObject);
            gameManager.draggedCard = null;
        }
        isDragged = false;
        gameManager.draggingCard = false;
        gameManager.draggedCard = null;
    }
    bool returningToDefaultPos;

    void ReturnToDefaultPosition()
    {
        if (Mathf.Approximately(transform.localPosition.x, defaultPosition.x))
        {
            // Is at default pos
            returningToDefaultPos = false;
        }
        else
        {
            returningToDefaultPos = true;
            transform.localPosition = Vector3.Lerp(transform.localPosition, defaultPosition, 1f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

         // Use Input.mousePosition to get the correct screen-space mouse position
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
        if (isDragged)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the object stays in the same plane
            transform.position = mousePosition;
        }
        else if (!isDragged)
        {
            ReturnToDefaultPosition();
        }
    }

}
