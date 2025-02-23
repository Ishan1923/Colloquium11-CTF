using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public bool GameIsPaused =false;
    // public Text timeText;
    public GameObject PauseMenu;
    public float timesScale = 1f;
    float time = 0;
    public GameObject InstrucMenu;
    // public GameObject timerObject;
    // public Text namepl;
    void Start()
    {
        // Time.timeScale = 0f;
        // // namepl.text ="Hey "+ PlayerPrefs.GetString("Name","*");
        // if(!(SceneManager.GetActiveScene().name == "GameFB"))
        // {
        InstrucMenu.SetActive(true);
        // }
        // if((SceneManager.GetActiveScene().name == "6-10 Easy" )||(SceneManager.GetActiveScene().name == "11-15 Easy"))
        // {
        // PlayerPrefs.SetString("RunnerTimeScore","");
        // }
    }
    public void Update()
    {
        if(!(SceneManager.GetActiveScene().name == "GameFB"))
        {
             if(!InstrucMenu.activeSelf)
             {
                time += Time.deltaTime;
                //  Timer(time);
             }
             // Health();
             if(InstrucMenu.activeSelf)
             {
                 if(Input.anyKeyDown){
                //  timerObject.SetActive(true);
                 
                 InstrucMenu.SetActive(false);
                 Time.timeScale= 1f;
                }
               }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
                PauseMenu.SetActive(false);
            }
            else
            {
                Pause();
                 PauseMenu.SetActive(true);
                
            }
        }
    }
    // void Timer(float time)
    // {
    //     float min = Mathf.FloorToInt(time/60);
    //     float sec = Mathf.FloorToInt(time%60);
        
    //     timeText.text = string.Format("Time Elapsed: " + "{0:00}:{1:00}", min, sec);
        
    //     // PlayerPrefs.SetString("MazeTime", timeText.text);
          
    // }
    public void QuitToMenu()
    {
            PauseMenu.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
    // public void QuitRunnerMenu()
    // {
    //   SceneManager.LoadScene("Runner Start");
    // }
    public void Pause()
    {  
      timesScale = Time.timeScale;
      Time.timeScale = 0f;
      GameIsPaused = true;

    }
    public void Resume()
    {
      PauseMenu.SetActive(false);
    GameIsPaused=false;
    Time.timeScale= timesScale;
    

    }
    public void Restart()
    {   
        GameIsPaused = false; 
         PauseMenu.SetActive(false);
        Time.timeScale= 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Play610Hard()
    {
      SceneManager.LoadScene("6-10 Hard");
    }
    public void Play1115Hard()
    {
      SceneManager.LoadScene("11-15 Hard");
    }
    public void QuitFB()
    {
      Debug.Log("Quit");
      Application.Quit();
      
    }

 }
