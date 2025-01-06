using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    private bool gameOver = false;

    public GameObject columnPrefab; // Prefab de la columna
    private float minHeight = 1.56f; // Altura mínima donde puede aparecer la columna
    private float maxHeight = -1.3f;  // Altura máxima donde puede aparecer la columna
    public float intervaloSpawn = 5.7f; // Tiempo entre apariciones de columnas

    private float spawnX = 10f;  // Posición fija en X para las columnas

    // Método para instanciar una nueva columna
    void SpawnColumn()
    {
        if (!gameOver)
        {
            // Calcula una posición aleatoria en el eje Y dentro del rango definido
            float randomHeight = Random.Range(minHeight, maxHeight);

            // Usa una posición fija en X y la altura aleatoria en Y
            Vector3 spawnPosition = new Vector3(spawnX, randomHeight, 0);  // Usa spawnX para X y randomHeight para Y
            Instantiate(columnPrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        CancelInvoke("SpawnColumn"); // Deja de invocar la creación de columnas
    }

    public void Restart()
    {
        gameOver = false;
        InvokeRepeating("SpawnColumn", 0f, intervaloSpawn);
    }
}