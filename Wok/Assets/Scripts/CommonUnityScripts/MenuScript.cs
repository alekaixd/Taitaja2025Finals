using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScript : MonoBehaviour
{
    public Slider slider;
    public MusicScript musicScript;
    public InfoContainer infoContainer;
    public TMP_Text audioText;
    void Awake()
    {
        infoContainer = FindFirstObjectByType<InfoContainer>();
    }
    void Update()
    {

    }
    public void UpdateVolume()
    {
        if (SceneManager.GetSceneByBuildIndex(0) == SceneManager.GetActiveScene())
        {
            musicScript.MasterVolume = slider.value/100;
            musicScript.UpdateVolume();
            infoContainer.MasterVolume = slider.value/100;
            audioText.text = slider.value.ToString();
        }
        
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
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetGameplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public bool mousePressed;
    public bool startedMousePress;
    public bool canceledMousePress;
}
