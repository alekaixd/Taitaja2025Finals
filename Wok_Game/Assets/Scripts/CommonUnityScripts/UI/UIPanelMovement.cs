using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// lerps UI objects to a destination position
/// 
/// uses animation curves to control the movement
/// 
/// call moveToDestination() and movetoStart() to move the object
/// </summary>


public class UIPanelMovement : MonoBehaviour
{
    public Vector2 DestinationPos, DefaultPos;
    public bool MovingToDestination, MovingToStart, ShouldScale;
    float PanelAlpha, directionModifier;
    public bool directionReversed;
    public float moveSpeed = 750;
    public AnimationCurve moveCurve;
    public AnimationCurve fadeCurve;
    public AnimationCurve scaleUpCurve;
    public AnimationCurve scaleDownCurve;
    private float moveTime;
    private float moveDuration = 1f; // Duration of the move in seconds
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = DefaultPos;
        rectTransform.localScale = Vector3.one; // Ensure the object starts at normal scale
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (directionReversed)
        {
            directionModifier = -1;
        }
        else
        {
            directionModifier = 1;
        }

        if (ShouldScale)
        {
            float scaleValue;
            if (MovingToDestination)
            {
                scaleValue = scaleDownCurve.Evaluate(moveTime);
                rectTransform.localScale = new Vector3(scaleValue, scaleValue, 1);
            }
            else if (MovingToStart)
            {
                scaleValue = scaleUpCurve.Evaluate(moveTime);
                rectTransform.localScale = new Vector3(scaleValue, scaleValue, 1);
            }
        }

        if (MovingToDestination)
        {
            moveTime += Time.deltaTime / moveDuration;
            float curveValue = moveCurve.Evaluate(moveTime);
            rectTransform.anchoredPosition = Vector2.Lerp(DefaultPos, new Vector2(DestinationPos.x * directionModifier, DestinationPos.y), curveValue);

            if (moveTime >= 1f)
            {
                MovingToDestination = false;
                moveTime = 0f;
            }
        }
        else if (MovingToStart)
        {
            moveTime += Time.deltaTime / moveDuration;
            float curveValue = moveCurve.Evaluate(moveTime);
            rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(DestinationPos.x * directionModifier, DestinationPos.y), DefaultPos, curveValue);

            if (moveTime >= 1f)
            {
                MovingToStart = false;
                moveTime = 0f;
            }
        }
    }

    public void MoveToDestination()
    {
        MovingToDestination = true;
        MovingToStart = false;
        moveTime = 0f;
    }

    public void MovetoStart()
    {
        MovingToStart = true;
        MovingToDestination = false;
        moveTime = 0f;
    }
}
