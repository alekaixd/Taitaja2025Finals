using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public MoveScript settingMoveScript, songMenuMoveScript, numpadMoveScript, titleMoveScript, modsMoveScript, discordMoveScipt;
    public MusicScript musicScript;
    public InfoContainer infoContainer;
    void Start()
    {
    }
    void Update()
    {

    }
    public void QuitToDesktop()
    {
        // Should add a "Are you sure you want to exit?"-prompt
        Debug.Log("Quit to Desktop");
        Application.Quit();
    }
    
    public void Volume(string Change)
    {
        if (inputScript.gamestate == 2 || inputScript.gamestate == 5) // If in game or endscreen, don't change volume
        {
            return;
        }
        if (Change == "up")
        {
            settingScript.SetMasterVolume(2);
            // Increase volume by 10%
        }
        else // If the change is not up, it's down, duh
        {
            settingScript.SetMasterVolume(3);
            // Decrease Volume by 10%
        }
    }
    public void Pause()
    {
        
        musicScript.PauseMusic();
        
    }

    public void Resume()
    {
        
        musicScript.ResumeMusic();
    }

    private IEnumerator ResumeGameplay()
    {
        musicScript.ResumeMusic();
    }

    public void EndScreen()
    {
        
    }

    public void ReturnToMainMenu()
    {

    }

    public void ResetGameplay()
    {

    }

}
