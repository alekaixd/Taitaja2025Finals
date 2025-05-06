using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public float MasterVolume, MusicVolume, HitsoundVolume;
    int FPS;
    public Slider MasterSlider, MusicSlider;
    public MusicScript musicScript;
    public InfoContainer infoContainer;

    public Settings savedSettings = new();
    void Start()
    {
        MasterSlider.value = MasterVolume;
        MusicSlider.value = MusicVolume;
        MasterInputField.text = MasterVolume.ToString();
        MusicInputField.text = MusicVolume.ToString();
        infoContainer.MasterVolume = MasterVolume;
        infoContainer.MusicVolume = MusicVolume;
        
    }

    /// <summary>
    /// Called when the FPS setting is changed.
    /// </summary>
    public void OnFPSChange()
    {
        FPS = int.Parse(FPSInputField.text);
        infoContainer.fps = FPS;
        Application.targetFrameRate = FPS;
        
        SaveSettings();
    }

    /// <summary>
    /// Sets the volume based on the provided parameters.
    /// </summary>
    /// <param name="which">Indicates the source of the change (slider or input field).</param>
    /// <param name="volume">Reference to the volume variable to update.</param>
    /// <param name="slider">The slider UI element.</param>
    /// <param name="inputField">The input field UI element.</param>
    /// <param name="updateVolumeAction">Action to update the volume.</param>
    public void SetVolume(int which, ref float volume, Slider slider, TMP_InputField inputField, Action<float> updateVolumeAction)
    {
        
        switch (which)
        {
            case 0: // If changed by slider, update field
                volume = slider.value;
                inputField.text = volume.ToString();
                break;
            case 1: // If changed by input field, update slider
                volume = float.Parse(inputField.text);
                slider.value = volume;
                break;
            case 2: // Update Both, field and slider at start
                inputField.text = volume.ToString();
                slider.value = volume;
                break;
        }
        updateVolumeAction(volume);
        musicScript.UpdateVolume();
        SaveSettings();
    }

    /// <summary>
    /// Sets the master volume.
    /// </summary>
    /// <param name="which">Indicates the source of the change (slider or input field).</param>
    public void SetMasterVolume(int which)
    {
        // This gets called by the sliders and input fields
        SetVolume(which, ref MasterVolume, MasterSlider, MasterInputField, (v) => musicScript.MasterVolume = v);
        infoContainer.MasterVolume = MasterVolume;
        SaveSettings();
    }

    /// <summary>
    /// Sets the music volume.
    /// </summary>
    /// <param name="which">Indicates the source of the change (slider or input field).</param>
    public void SetMusicVolume(int which)
    {
        // This gets called by the sliders and input fields
        SetVolume(which, ref MusicVolume, MusicSlider, MusicInputField, (v) => musicScript.MusicVolume = v);
        infoContainer.MusicVolume = MusicVolume;
        SaveSettings();
    }
}
