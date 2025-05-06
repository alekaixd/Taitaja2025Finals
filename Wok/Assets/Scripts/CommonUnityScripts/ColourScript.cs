using UnityEngine;

public class ColourScript : MonoBehaviour
{
    public Color returnColour;
    public Color[] colours;
    public int currentColourIndex = 0;
    public int targetColourIndex = 1;
    private float targetPoint;
    public float time;

    void FixedUpdate()
    {
        TransitionColour();
    }

    /// <summary>
    /// Transitions the color over time.
    /// </summary>
    public void TransitionColour()
    {
        targetPoint += Time.deltaTime / time;
        returnColour = Color.Lerp(colours[currentColourIndex], colours[targetColourIndex], targetPoint);
        if(targetPoint >= 1)
        {
            targetPoint = 0;    
            currentColourIndex = targetColourIndex;
            targetColourIndex++;
            if (targetColourIndex >= colours.Length)
            {
                targetColourIndex = 0;
            }
        }
    }
}
