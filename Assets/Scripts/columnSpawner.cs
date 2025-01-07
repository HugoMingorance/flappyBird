using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    private bool gameOver = false;  // Indica si el joc ha acabat

    public GameObject columnPrefab; // Prefab de la columna
    private float minHeight = 1.56f; // Alçada mínima on pot aparèixer la columna
    private float maxHeight = -1.3f;  // Alçada màxima on pot aparèixer la columna
    public float intervaloSpawn = 5.7f; // Temps entre aparicions de columnes

    private float spawnX = 10f;  // Posició fixa en X per a les columnes

    // Mètode per instanciar una nova columna
    void SpawnColumn()
    {
        if (!gameOver)
        {
            // Calcula una posició aleatòria en l'eix Y dins del rang definit
            float randomHeight = Random.Range(minHeight, maxHeight);

            // Utilitza una posició fixa en X i l'alçada aleatòria en Y
            Vector3 spawnPosition = new Vector3(spawnX, randomHeight, 0);  // Usa spawnX per a X i randomHeight per a Y
            Instantiate(columnPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Mètode per gestionar l'estat de Game Over
    public void GameOver()
    {
        gameOver = true;
        CancelInvoke("SpawnColumn"); // Deixa d'invocar la creació de columnes
    }

    // Mètode per reiniciar la creació de columnes
    public void Restart()
    {
        gameOver = false;
        InvokeRepeating("SpawnColumn", 0f, intervaloSpawn);
    }
}