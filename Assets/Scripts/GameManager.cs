using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int Score = 0;
    int BestScore = 0;
    private GameObject clSpawner;
    private GameObject[] columns;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI menuText;
    public TextMeshProUGUI gameOverText;

    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        clSpawner = GameObject.FindGameObjectWithTag("clSpawner");
        columns = GameObject.FindGameObjectsWithTag("columns");
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + BestScore;
        menuText.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)))
        {
            StartGame();
        }

        if (gameStarted)
        {
            scoreText.text = "Score: " + Score;
        }
        else if (gameOverText.gameObject.activeSelf && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)))
        {
            RestartGame();
        }
    }

    public void incrementScore()
    {
        Score++;
        Debug.Log(Score);
    }

    public void GameOver()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
        Debug.Log("GameOver");
        menuText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);

        // Detener las columnas y el fondo
        clSpawner.GetComponent<ColumnSpawner>().GameOver();
        foreach (GameObject column in GameObject.FindGameObjectsWithTag("columns"))
        {
            column.GetComponent<ColumnScript>().GameOver();
        }
        foreach (GameObject background in GameObject.FindGameObjectsWithTag("background"))
        {
            background.GetComponent<BackgroundScript>().GameOver();
        }
    }

    public void RestartGame()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartGame()
    {
        gameStarted = true;
        menuText.gameObject.SetActive(false);
        clSpawner.GetComponent<ColumnSpawner>().InvokeRepeating("SpawnColumn", 0f, clSpawner.GetComponent<ColumnSpawner>().intervaloSpawn);
        GameObject bird = GameObject.FindGameObjectWithTag("bird");
        bird.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}