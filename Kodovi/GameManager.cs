using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveScore;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text livesText;
    public Image fadeImage;

    private Blade blade;
    private Spawner spawner;
    private HealthBar healthBar;

    public Data saveData;

    private int score;
    public int highScore;
    public int index;

    int lives = 3;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        healthBar = FindObjectOfType<HealthBar>();

        // Get high score from json file
        saveData = SaveScore.LoadMyData();
        highScore = saveData.highScore;
        Debug.Log(highScore);

        // Get color data from json file and change blade color to that color
        index = saveData.index;
        Debug.Log(index);
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        healthBar.SetHealt(lives);

        livesText.text = "Lives: " + lives;
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        highScoreText.text = "Best: " + highScore.ToString();

        ClearScene();
    }

    //ciscenje scene i priprema za novu igru
    public void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject); //unisti sva voca u igri
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject); //unisti sve bombe u igri
        }
    }

    public void IncreaseScore()
    {
        if(score < highScore)
        {
            score++;
            scoreText.text = score.ToString();
        } 
        else
        {
            score++;
            highScore++;
            scoreText.text = score.ToString();
            highScoreText.text = "Best: " + highScore.ToString();
        }
    }

    public void Explode()
    {
        lives--;
        healthBar.SetHealt(lives);

        if (lives <= 0)
        {
            blade.enabled = false;
            spawner.enabled = false;
            StartCoroutine(ExplodeSequence());
        }
        livesText.text = "Lives: " + lives.ToString();
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration); // % koliko je vrijeme animacije ostalo

            //mijenjamo boju animacije ovisno o vremenu koliko je ostalo vremena(kada je t=1 boja je bijela)
            //kada je t = 0, boja je clear
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t); 
            Time.timeScale = 1f - t; // ovo prakticki pretvara animaciju da bude u slowmotionu, usporava je
            elapsed += Time.unscaledDeltaTime;
            yield return null; //cekaj do sljedeceg framea
        }

        yield return new WaitForSecondsRealtime(1f); //pricekaj 1 sekundu

        NewGame();
        // povratak iz bijelog u igru
        elapsed = 0; 

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);

            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
