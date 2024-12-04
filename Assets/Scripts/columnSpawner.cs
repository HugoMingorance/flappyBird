using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public GameObject columnPrefab; // Prefab de la columna
    public float intervaloSpawn = 2f; // Tiempo entre apariciones de columnas
    private float tiempoUltimoSpawn;

    // Start is called before the first frame update
    void Start()
    {
        tiempoUltimoSpawn = Time.time; // Inicia el contador del tiempo para el primer spawn
    }

    // Update is called once per frame
    void Update()
    {
        // Verificamos si ha pasado el intervalo para hacer aparecer una nueva columna
        if (Time.time - tiempoUltimoSpawn >= intervaloSpawn)
        {
            SpawnColumn(); // Instanciamos una nueva columna
            tiempoUltimoSpawn = Time.time; // Reiniciamos el contador de tiempo
        }
    }

    // Método para instanciar una nueva columna
    void SpawnColumn()
    {
        // Instanciamos la columna en la misma posición que el prefab original
        Instantiate(columnPrefab, columnPrefab.transform.position, Quaternion.identity);
    }
}
