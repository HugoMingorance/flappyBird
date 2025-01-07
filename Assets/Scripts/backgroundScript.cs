using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float velocidad = 2f; // Velocitat del moviment
    public float anchoPantalla; // Amplada de la pantalla

    private Vector2 posicionOriginal; // Posició original del fons
    private bool gameOver = false; // Indica si el joc ha acabat

    // Mètode Start: s'executa al començament del joc
    void Start()
    {
        anchoPantalla = GetComponent<SpriteRenderer>().bounds.size.x; // Obté l'amplada de la imatge
        posicionOriginal = transform.position; // Guarda la posició inicial
    }

    // Mètode Update: s'executa a cada frame
    void Update()
    {
        if (!gameOver)
        {
            // Mou el fons cap a l'esquerra
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);

            // Quan la imatge surt de la pantalla, es reposiciona
            if (transform.position.x <= -anchoPantalla)
            {
                transform.position = new Vector2(transform.position.x + anchoPantalla * 2, transform.position.y);
            }
        }
    }

    // Mètode GameOver: gestiona l'estat de Game Over
    public void GameOver()
    {
        gameOver = true; // Atura el moviment del fons
    }

    // Mètode Restart: reinicia el moviment del fons
    public void Restart()
    {
        gameOver = false;
    }
}