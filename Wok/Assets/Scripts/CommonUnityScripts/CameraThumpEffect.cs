using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraThumpEffect : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomInAmount;
    public float zoomOutSpeed;
    public float bpm;
    [Header("Bloom Settings - Overwrites urp settings")]
    public float bloomScatter;
    public float bloomThreshold;
    public float bloomDefaultIntensity;
    public float bloomThumpMultiplier;

    private float originalSize;
    private float thumpInterval;
    private float nextThumpTime;
    private Bloom bloom;
    private Coroutine thumpCoroutine;
    private bool isThumping = true;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        
        originalSize = mainCamera.orthographicSize;
        thumpInterval = (bpm / 60) * 4;
        nextThumpTime = Time.time + thumpInterval;

        Volume volume = mainCamera.GetComponent<Volume>();
        if (volume == null)
        {
            volume = mainCamera.gameObject.AddComponent<Volume>();
            volume.isGlobal = true;
            volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();
        }

        if (!volume.profile.TryGet<Bloom>(out bloom))
        {
            bloom = volume.profile.Add<Bloom>(true);
        }

        // Set initial bloom settings
        bloom.intensity.value = bloomDefaultIntensity;
        bloom.scatter.value = bloomScatter;
        bloom.threshold.value = bloomThreshold;
    }

    public void FixedUpdate()
    {
        if (Time.time >= nextThumpTime && isThumping)
        {
            thumpCoroutine = StartCoroutine(Thump());
            nextThumpTime = Time.time + thumpInterval;
        }
    }

    /// <summary>
    /// Updates the BPM and recalculates the thump interval.
    /// </summary>
    /// <param name="newBPM">The new BPM value.</param>
    public void updateBPM(float newBPM)
    {
        bpm = newBPM;
        nextThumpTime = 0;
        thumpInterval = 60.0f / bpm;
    }

    /// <summary>
    /// Coroutine that handles the thumping effect.
    /// </summary>
    private IEnumerator Thump() // It's thumping time
    {
        mainCamera.orthographicSize = originalSize * zoomInAmount;
        if (bloom != null)
        {
            bloom.intensity.value = bloomDefaultIntensity * bloomThumpMultiplier;
        }
        yield return new WaitForSeconds(0.1f);
        while (mainCamera.orthographicSize < originalSize)
        {
            mainCamera.orthographicSize += zoomOutSpeed * Time.deltaTime;
            if (bloom != null)
            {
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, bloomDefaultIntensity, zoomOutSpeed * Time.deltaTime);
            }
            yield return null;
        }
        mainCamera.orthographicSize = originalSize;
        if (bloom != null)
        {
            bloom.intensity.value = bloomDefaultIntensity;
        }
    }

    /// <summary>
    /// Stops the thumping effect and resets the camera and bloom settings.
    /// </summary>
    public void StopThump()
    {
        if (thumpCoroutine != null)
        {
            isThumping = false;
            StopCoroutine(thumpCoroutine);
            thumpCoroutine = null;
        }
        mainCamera.orthographicSize = originalSize;
        if (bloom != null)
        {
            bloom.intensity.value = bloomDefaultIntensity;
        }
    }

    /// <summary>
    /// Starts the thumping effect.
    /// </summary>
    public void StartThump()
    {
        isThumping = true;
        nextThumpTime = Time.time;
    }
}
