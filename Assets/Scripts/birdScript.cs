using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 3f;  // Fuerza que se aplicará al pájaro al tocar la pantalla
    private Rigidbody2D rb;
    public GameObject gm;
    private bool GameOver = false;
    private bool gameStarted = false;

    // Ángulo máximo y mínimo de inclinación
    private float maxAngle = 20f;  // Ángulo máximo hacia arriba (ajustado)
    private float minAngle = -45f;  // Ángulo mínimo hacia abajo (ajustado)

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtenemos el componente Rigidbody2D del pájaro
        gm = GameObject.FindGameObjectWithTag("gm");
        rb.isKinematic = true;  // Hacemos que el pájaro no se mueva al inicio
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            rb.isKinematic = false;  // Permitimos que el pájaro se mueva
        }

        if (gameStarted && !GameOver)
        {
            // Detectar un nuevo toque
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.up * fuerzaSalto;  // Aplicamos fuerza hacia arriba al Rigidbody2D
            }

            // Calcular el ángulo basado en la velocidad vertical
            float angle = Mathf.Atan2(rb.velocity.y, 10) * Mathf.Rad2Deg;  // Cambia 10 para ajustar sensibilidad

            // Limitar el ángulo entre los valores máximo y mínimo
            angle = Mathf.Clamp(angle, minAngle, maxAngle);

            // Aplicar la rotación al pájaro
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe") || collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("ceiling"))
        {
            GameOver = true;
            gm.GetComponent<GameManager>().GameOver();
            Debug.Log("Colisión con un pipe");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            gm.GetComponent<GameManager>().incrementScore();
            Debug.Log("Goal");
        }
    }
}