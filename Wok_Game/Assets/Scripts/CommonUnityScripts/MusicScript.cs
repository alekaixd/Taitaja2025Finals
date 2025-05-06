using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public AudioSource CurrentSong, HitSounds, MissSound, ButtonTap, ButtonLift;
    public float MasterVolume, MusicVolume, HitsoundVolume, songPosition;
    private InfoContainer infoContainer;
    public EditorScript editorScript;
    public LineScript lineScript;
    void FixedUpdate()
    {
        if (CurrentSong.isPlaying)
        {
            songPosition = CurrentSong.time;
        }
    }
    void Start()
    {
        // Try to find the InfoContainer
        infoContainer = GameObject.Find("InfoContainer").GetComponent<InfoContainer>();
        if (infoContainer == null)
        {
            throw new System.Exception("InfoContainer not found.");
        }
        
        
        if (SceneManager.GetActiveScene().buildIndex == 0) // Get setting manager if in main menu
        {
            SettingScript settingManager = GameObject.Find("SettingManager").GetComponent<SettingScript>();
            MasterVolume = settingManager.MasterVolume;
            MusicVolume = settingManager.MusicVolume;
            HitsoundVolume = settingManager.HitsoundVolume;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)// Else find EditorManager
        {
            var editorManager = GameObject.Find("EditorManager").GetComponent<EditorScript>();
            if (editorManager != null)
            {
                MasterVolume = editorManager.MasterVolume;
                MusicVolume = editorManager.MusicVolume;
                HitsoundVolume = editorManager.HitsoundVolume;
            }
            else
            {
                throw new System.Exception("EditorManager not found.");
            }
        }
        CurrentSong.volume = MusicVolume / 100 * (MasterVolume / 100);
        HitSounds.volume = HitsoundVolume / 100 * (MasterVolume / 100);
        CurrentSong.clip = infoContainer.SelectedSong;
        UpdateVolume();
    }
    // This script handles only ingame audio, not editor audio, because the application of the editor is a lot different to what this provides

    /// <summary>
    /// Updates the audio track with the provided clip.
    /// </summary>
    /// <param name="track">The new audio clip.</param>
    public void UpdateAudioTrack(AudioClip track)
    {
        CurrentSong.clip = track;
    }

    /// <summary>
    /// Plays the music with the specified pitch.
    /// </summary>
    /// <param name="pitch">The pitch to play the music at.</param>
    public void PlayMusic(float pitch = 1.0f)
    {
        CurrentSong.volume = MusicVolume / 100 * (MasterVolume / 100);
        CurrentSong.pitch = pitch;
        CurrentSong.Play();
        CurrentSong.loop = false;
    }

    /// <summary>
    /// Plays a preview of the music starting at the specified point.
    /// </summary>
    /// <param name="previewPoint">The point to start the preview from.</param>
    public void PlayMusicPreview(float previewPoint)
    {
        CurrentSong.volume = MusicVolume / 100 * (MasterVolume / 100);
        CurrentSong.Play();
        CurrentSong.time = previewPoint;
        CurrentSong.loop = true;
    }

    /// <summary>
    /// Pauses the music.
    /// </summary>
    public void PauseMusic()
    {
        CurrentSong.Pause();
    }

    /// <summary>
    /// Resumes the music.
    /// </summary>
    public void ResumeMusic()
    {
        CurrentSong.UnPause();
    }

    /// <summary>
    /// Plays the hitsound.
    /// </summary>
    public void PlayHitsound()
    {
        HitSounds.volume = MasterVolume / 100;
        HitSounds.Play();
    }

    /// <summary>
    /// Plays the miss sound.
    /// </summary>
    public void PlayMissSound()
    {
        MissSound.volume = MasterVolume / 100;
        MissSound.Play();
    }
    public void PlayTap()
    {
        ButtonTap.volume = MasterVolume / 100;
        ButtonTap.Play();
    }
    public void PlayLift()
    {
        ButtonLift.volume = MasterVolume / 100;
        ButtonLift.Play();
    }
    /// <summary>
    /// Stops the music.
    /// </summary>
    public void StopMusic()
    {
        CurrentSong.Stop();
        CurrentSong.pitch = 1.0f;
    }

    /// <summary>
    /// Sets the song position to the start.
    /// </summary>
    public void SetSongPositionToStart()
    {
        CurrentSong.time = 0;
    }

    /// <summary>
    /// Sets the song position based on the specified percentage.
    /// </summary>
    /// <param name="positionPercentage">The percentage of the song to set the position to.</param>
    public void SetSongPosition(float positionPercentage)
    {
        if (CurrentSong.clip != null)
        {
            CurrentSong.time = CurrentSong.clip.length * positionPercentage;
        }
    }

    /// <summary>
    /// Updates the volume of the audio sources.
    /// </summary>
    public void UpdateVolume()
    {
        CurrentSong.volume = MusicVolume / 100 * (MasterVolume / 100);
    }
}