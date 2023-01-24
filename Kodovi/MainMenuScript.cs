using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuUI;
    public float bombChanceFloat = 0.1f;

    private void Awake()
    {
        bombChanceFloat = 0.1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("FruitNinja");
    }

    public void openCustomizationScreen()
    {
        SceneManager.LoadScene("Theme");
    }

    public void setEasy() 
    {
        bombChanceFloat = 0.1f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat); //Postavi sansu za bomb spawn na 10%
    }

    public void setMedium()
    {
        bombChanceFloat = 0.25f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat); //Postavi sansu za bomb spawn na 25%

    }

    public void setHard()
    {
        bombChanceFloat = 0.4f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat); //Postavi sansu za bomb spawn na 40%
    }
}
