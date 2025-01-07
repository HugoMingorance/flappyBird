using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 3f;  // Fuerza que se aplicará al pájaro al tocar la pantalla
    private Rigidbody2D rb;
    private Animator animator;  // Referencia al componente Animator
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
        animator = GetComponent<Animator>();  // Obtenemos el componente Animator
        gm = GameObject.FindGameObjectWithTag("gm");
        rb.isKinematic = true;  // Hacemos que el pájaro no se mueva al inicio

        // Asegurarse de que la animación "bird" esté activa al inicio
        animator.Play("bird");
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
                gm.GetComponent<GameManager>().PlaySound("jump");  // Reproducir sonido de salto
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
            animator.SetTrigger("dead");  // Activar el trigger de la animación "dead"
            rb.velocity = new Vector2(0, rb.velocity.y);  // Opcional: ajustar la velocidad horizontal a 0 para que no se desplace horizontalmente
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