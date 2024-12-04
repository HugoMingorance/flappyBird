using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    private float velocidad = 2f; // Velocidad del movimiento
    public float anchoPantalla; // El ancho de la pantalla

    private Vector2 posicionOriginal;

    // Start is called before the first frame update
    void Start()
    {
        anchoPantalla = GetComponent<SpriteRenderer>().bounds.size.x; // Obtiene el ancho de la imagen
        posicionOriginal = transform.position; // Guarda la posición inicial
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve el fondo hacia la izquierda
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);

        // Cuando la imagen sale de la pantalla, se reposiciona
        if (transform.position.x <= -anchoPantalla)
        {
            transform.position = new Vector2(transform.position.x + anchoPantalla * 2, transform.position.y);
        }
    }
}
