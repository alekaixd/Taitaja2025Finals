using UnityEngine;
using UnityEngine.UI;

public class ButtonPressEffect : MonoBehaviour
{
    public Image targetImage; // The image to scale
    public AnimationCurve scaleCurve; // The animation curve for scaling
    public AnimationCurve descaleCurve; // The animation curve for descaling
    public float duration = 1f; // Duration of the scaling effect

    private Vector3 originalScale;
    private float timer;
    private bool isScaling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (targetImage != null)
        {
            originalScale = targetImage.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isScaling)
        {
            timer += Time.deltaTime;
            float scaleValue = scaleCurve.Evaluate(timer / duration);
            targetImage.transform.localScale = originalScale * scaleValue;

            if (timer >= duration)
            {
                timer = duration; // Clamp timer to duration
            }
        }
        else
        {
            timer += Time.deltaTime;
            float scaleValue = descaleCurve.Evaluate(timer / duration);
            targetImage.transform.localScale = originalScale * scaleValue;

            if (timer >= duration)
            {
                timer = duration; // Clamp timer to duration
                targetImage.transform.localScale = originalScale;
            }
        }
    }

    public void StartScaling()
    {
        if (targetImage != null)
        {
            isScaling = true;
            timer = 0;
        }
    }

    public void StopScaling()
    {
        if (targetImage != null)
        {
            isScaling = false;
            timer = 0;
        }
    }
}
