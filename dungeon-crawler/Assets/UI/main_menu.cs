using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu : MonoBehaviour
{
     public static bool curPaus = false;
    public GameObject pauseMenu;
    public GameObject quitButton;
    public GameObject startButton;

    void Start()
    {
        
    }
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)){
            Debug.Log("pressed");
            if (curPaus){
                Resume();
            } else {
                Pause();
            }
        }
    }


    public void start() { }

    public void options() { }

    public void quitgame() {
     Application.Quit();  
    }
     public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void startLobby(){
        SceneManager.LoadScene(0);
    }
    void Resume(){
        pauseMenu.SetActive(false);
        quitButton.SetActive(true);
        startButton.SetActive(true);
        Time.timeScale = 1f;
        curPaus = false;
    }
    void Pause(){
        pauseMenu.SetActive(true);
        quitButton.SetActive(false);
        startButton.SetActive(false);
        Time.timeScale = 0f;
        curPaus = true;
    }

}