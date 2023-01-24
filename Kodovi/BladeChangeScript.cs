using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveScore;
using static PauseMenu;
using System.Security.Cryptography;

public class BladeChangeScript : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Toggle[] toggles;
    public Data saveData;
    public int index;

    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>(); //Neka varijabla toggeGroup dobije komponentu mijenjanja stanja
    }

    // funkcija za promjenu boja
    public void ChangeColor()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                Debug.Log(i);
                index = i;
            }
        }
    }

    // potvrda boje u save file
    public void ConfirmColor()
    {
        // Dodaj boju u save file
        Debug.Log(index);

        // Dodaj u save file index
        saveData.index = index;
        int highScore = PlayerPrefs.GetInt("highscore");
        saveData.highScore = highScore;
        SaveScore.SaveMyData(saveData);

        // Promini scenu
        SceneManager.LoadScene("Menu");
    }
}
