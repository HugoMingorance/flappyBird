using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScript : MonoBehaviour
{
    private float velocidad = 2f; // Velocitat del moviment
    private bool gameOver = false;

    // Mètode Update: s'executa a cada frame
    void Update()
    {
        if (!gameOver)
        {
            // Mou les columnes
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);

            // Destrueix l'objecte columna quan surt de la pantalla
            if (transform.position.x <= -5)
            {
                Destroy(gameObject);
            }
        }
    }

    // Mètode GameOver: atura el moviment de les columnes
    public void GameOver()
    {
        gameOver = true;
    }
}