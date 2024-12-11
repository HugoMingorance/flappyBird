using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnScript : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad del movimiento
    private Vector2 posicionOriginal;

    // Start is called before the first frame update
    void Start()
    {
        posicionOriginal = transform.position; // Guarda la posición inicial
    }

    // Update is called once per frame
    void Update()
    {
        // Mueve las columnas
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);
        if(transform.position.x <= -5){
            gameObject.SetActive(false);
        }
    }

}
