using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public AudioSource CurrentSong, HissSounds, ButtonTap, ButtonLift;
    public float MasterVolume, songPosition;
    private InfoContainer infoContainer;
    void Awake()
    {
        infoContainer = FindFirstObjectByType<InfoContainer>();
    }
    void Start()
    {
        MasterVolume = infoContainer.MasterVolume;
        CurrentSong.volume = infoContainer.MasterVolume;
        //HitSounds.volume = HitsoundVolume / 100 * (MasterVolume / 100);
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
        CurrentSong.volume =MasterVolume / 100;
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
        CurrentSong.volume =MasterVolume / 100;
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
        HissSounds.volume = MasterVolume / 100;
        HissSounds.Play();
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
        CurrentSong.volume = MasterVolume;
    }
}