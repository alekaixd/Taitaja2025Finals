using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuScript : MonoBehaviour
{
    public MusicScript musicScript;
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
    
    public void Pause()
    {
        
        musicScript.PauseMusic();
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
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
    public InputAction.CallbackContext MouseState;
    public void MousePress(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse state: " + context.phase);
        MouseState = context;
    }
}
