using UnityEngine;
using UnityEngine.UI;

public class BackgroundDimScript : MonoBehaviour
{
    public int dimValue = 51;
    public float lerpDuration = 0.5f; // Duration of the lerp
    private bool startDim = false;
    private bool startUndim = false;
    private Color targetColor;
    private Color initialColor;
    private float lerpTime;
    private RawImage rawImage;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        initialColor = rawImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDim && lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;
            rawImage.color = Color.Lerp(initialColor, targetColor, lerpTime / lerpDuration);
        }
        else if (startUndim && lerpTime < lerpDuration)
        {
            lerpTime += Time.deltaTime;
            rawImage.color = Color.Lerp(targetColor, initialColor, lerpTime / lerpDuration);
        }
        else if (lerpTime >= lerpDuration)
        {
            // Ensure the final color is set correctly
            if (startDim)
            {
                rawImage.color = targetColor;
            }
            else if (startUndim)
            {
                rawImage.color = initialColor;
            }

            // Reset lerpTime and flags when lerp is complete
            lerpTime = 0;
            startDim = false;
            startUndim = false;
        }
    }

    /// <summary>
    /// Dims the background over time.
    /// </summary>
    public void DimBackground()
    {
        if (startDim)
        {
            return;
        }
        startDim = true;
        startUndim = false;
        targetColor = new Color(0, 0, 0, dimValue / 255f);
        lerpTime = 0;
        Debug.Log("Dimming background");
    }

    /// <summary>
    /// Undims the background over time.
    /// </summary>
    public void UndimBackground()
    {
        if (startUndim)
        {
            return;
        }
        startUndim = true;
        startDim = false;
        targetColor = new Color(0, 0, 0, 0); // Assuming the original color has no dimming
        lerpTime = 0;
        Debug.Log("Undimming background");
    }
}
