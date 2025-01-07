using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    int Score = 0;  // Puntuació actual
    int BestScore = 0;  // Millor puntuació guardada
    private GameObject clSpawner;  // Objecte per a generar les columnes
    private GameObject[] columns;  // Array de les columnes generades

    public TextMeshProUGUI scoreText;  // Text de la puntuació actual
    public TextMeshProUGUI bestScoreText;  // Text de la millor puntuació
    public TextMeshProUGUI menuText;  // Text del menú inicial
    public TextMeshProUGUI gameOverText;  // Text de Game Over

    // Clips d'àudio
    public AudioClip jumpClip;  // So de salt
    public AudioClip scoreClip;  // So de puntuació
    public AudioClip gameOverClip;  // So de Game Over

    private AudioSource audioSource;  // Font d'àudio

    private bool gameStarted = false;  // Indica si el joc ha començat
    private bool gameOver = false;  // Indica si el joc ha acabat

    // Mètode Start: s'executa al començament del joc
    void Start()
    {
        clSpawner = GameObject.FindGameObjectWithTag("clSpawner");  // Trobar l'objecte que genera columnes
        columns = GameObject.FindGameObjectsWithTag("columns");  // Trobar totes les columnes
        BestScore = PlayerPrefs.GetInt("BestScore", 0);  // Recuperar la millor puntuació guardada
        bestScoreText.text = "Best Score: " + BestScore;  // Mostrar la millor puntuació
        menuText.gameObject.SetActive(true);  // Mostrar el menú inicial
        gameOverText.gameObject.SetActive(false);  // Amagar el text de Game Over

        audioSource = GetComponent<AudioSource>();  // Obtenir el component AudioSource
    }

    // Mètode Update: s'executa a cada frame
    void Update()
    {
        // Començar el joc si no ha començat encara i es detecta un toc o una tecla
        if (!gameStarted && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)))
        {
            StartGame();
        }

        // Reiniciar el joc si està en estat de Game Over i es detecta un toc o una tecla
        if (gameOver && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)))
        {
            RestartGame();
        }

        // Actualitzar el text de la puntuació si el joc ha començat
        if (gameStarted)
        {
            scoreText.text = "Score: " + Score;
        }
    }

    // Mètode incrementScore: incrementa la puntuació
    public void incrementScore()
    {
        Score++;  // Incrementar la puntuació
        scoreText.text = "Score: " + Score;  // Actualitzar el text de la puntuació
        Debug.Log(Score);  // Mostrar la puntuació al log
        PlaySound("score");  // Reproduir so de puntuació
    }

    // Mètode GameOver: gestiona l'estat de Game Over
    public void GameOver()
    {
        // Actualitzar la millor puntuació si la puntuació actual és més alta
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
        Debug.Log("GameOver");  // Mostrar missatge al log
        menuText.gameObject.SetActive(false);  // Amagar el menú inicial
        gameOverText.gameObject.SetActive(true);  // Mostrar el text de Game Over

        // Detenir les columnes i el fons
        clSpawner.GetComponent<ColumnSpawner>().GameOver();
        foreach (GameObject column in GameObject.FindGameObjectsWithTag("columns"))
        {
            column.GetComponent<ColumnScript>().GameOver();
        }
        foreach (GameObject background in GameObject.FindGameObjectsWithTag("background"))
        {
            background.GetComponent<BackgroundScript>().GameOver();
        }

        gameOver = true;  // Activar l'estat de Game Over
        PlaySound("gameover");  // Reproduir so de Game Over
    }

    // Mètode RestartGame: reinicia el joc
    public void RestartGame()
    {
        // Recargar l'escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Mètode StartGame: inicia el joc
    private void StartGame()
    {
        gameStarted = true;  // Indicar que el joc ha començat
        menuText.gameObject.SetActive(false);  // Amagar el menú inicial
        clSpawner.GetComponent<ColumnSpawner>().InvokeRepeating("SpawnColumn", 0f, clSpawner.GetComponent<ColumnSpawner>().intervaloSpawn);  // Començar a generar columnes
        GameObject bird = GameObject.FindGameObjectWithTag("bird");  // Trobar l'objecte del pájaro
        bird.GetComponent<Rigidbody2D>().isKinematic = false;  // Permetre que el pájaro es mogui
    }

    // Mètode PlaySound: reprodueix un so especificat
    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "jump":
                audioSource.PlayOneShot(jumpClip);  // Reproduir so de salt
                break;
            case "score":
                audioSource.PlayOneShot(scoreClip);  // Reproduir so de puntuació
                break;
            case "gameover":
                audioSource.PlayOneShot(gameOverClip);  // Reproduir so de Game Over
                break;
        }
    }
}