using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScript : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del movimiento
    public float rangoY = 3f; // Rango dentro del cual aparecerán los tubos (positivo y negativo)
    private float espacioEntrePipes = 12f; // Espacio mínimo entre los dos tubos
    public float limiteSuperior = 3f; // Limite superior en el eje Y para los tubos
    public float limiteInferior = -3f; // Limite inferior en el eje Y para los tubos

    private Vector2 posicionOriginal;

    // Start is called before the first frame update
    void Start()
    {
        posicionOriginal = transform.position; // Guarda la posición inicial

        // Asigna posiciones aleatorias a los pipes
        AsignarPosicionPipes();
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve las columnas
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);
    }

    // Método para asignar las posiciones a los tubos
    void AsignarPosicionPipes()
    {
        // Obtén el transform de los objetos hijos (pipe(1) y pipe(2))
        Transform pipe1 = transform.Find("pipe(1)");
        Transform pipe2 = transform.Find("pipe(2)");

        // Calcula una posición aleatoria para el pipe de abajo (pipe(1))
        float randomY = Random.Range(limiteInferior, limiteSuperior - espacioEntrePipes); // Asegura que no se salga del límite
        pipe1.position = new Vector2(transform.position.x, randomY); // Asigna la posición de pipe(1)

        // Calcula la posición para el pipe de arriba (pipe(2)), con suficiente espacio entre los dos
        pipe2.position = new Vector2(transform.position.x, randomY + espacioEntrePipes); // Asigna la posición de pipe(2)
    }
}
