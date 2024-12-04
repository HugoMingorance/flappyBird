using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 5f; // Fuerza que se aplicará al pájaro al tocar la pantalla
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D del pájaro
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) // Si se toca la pantalla
        {
            rb.velocity = Vector2.up * fuerzaSalto; // Aplicamos fuerza hacia arriba al Rigidbody2D
        }
    }
}
