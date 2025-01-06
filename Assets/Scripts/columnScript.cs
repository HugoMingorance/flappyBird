using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScript : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del movimiento
    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            // Mueve las columnas
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);
            if (transform.position.x <= -5)
            {
                Destroy(gameObject);
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
    }
}