using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScore;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; //varijabla za provjeru je li igra pauzirana

    public GameObject pauseMenuUI;
    public static GameManager manager;

    public Data saveData;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update se poziva svaki frame
    void Update()
    {
        //Ako je pritisnuta tipka Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Ako je igra pauzirana, odpauziraj je
            if(isPaused)
            {
                Resume();
            }

            //Ako nije pauziraj je
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //SKloni pauseMenu
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        //Kada pauziramo, prikazi pauseMenu
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //zaustavi vrijeme i igru
        isPaused = true;
    }

    public void LoadMenu()
    {
        saveData.highScore = manager.highScore;
        PlayerPrefs.SetInt("highscore", manager.highScore);
        
        // Store high score in json file
        SaveScore.SaveMyData(saveData);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        saveData.highScore = manager.highScore;
        PlayerPrefs.SetInt("highscore", manager.highScore);
        // Store high score in json file
        SaveScore.SaveMyData(saveData);
        Application.Quit();
    }
}
