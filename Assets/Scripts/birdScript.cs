using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 3f;  // Força que s'aplicarà al pájaro al tocar la pantalla
    private Rigidbody2D rb;
    private Animator animator;  // Referència al component Animator
    public GameObject gm;
    private bool GameOver = false;
    private bool gameStarted = false;

    // Angle màxim i mínim d'inclinació
    private float maxAngle = 20f;  // Angle màxim cap amunt (ajustat)
    private float minAngle = -45f;  // Angle mínim cap avall (ajustat)

    // Mètode Start: s'executa al començament del joc
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtenim el component Rigidbody2D del pájaro
        animator = GetComponent<Animator>();  // Obtenim el component Animator
        gm = GameObject.FindGameObjectWithTag("gm");
        rb.isKinematic = true;  // Fem que el pájaro no es mogui al començament

        // Assegurar-se que l'animació "bird" estigui activa al començament
        animator.Play("bird");
    }

    // Mètode Update: s'executa a cada frame
    void Update()
    {
        // Començar el joc si no ha començat encara i es detecta un clic
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            rb.isKinematic = false;  // Permetem que el pájaro es mogui
        }

        // Si el joc ha començat i no està en estat de Game Over
        if (gameStarted && !GameOver)
        {
            // Detectar un nou toc
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = Vector2.up * fuerzaSalto;  // Apliquem força cap amunt al Rigidbody2D
                gm.GetComponent<GameManager>().PlaySound("jump");  // Reproduir so de salt
            }

            // Calcular l'angle basat en la velocitat vertical
            float angle = Mathf.Atan2(rb.velocity.y, 10) * Mathf.Rad2Deg;  // Canviar 10 per ajustar sensibilitat

            // Limitar l'angle entre els valors màxim i mínim
            angle = Mathf.Clamp(angle, minAngle, maxAngle);

            // Aplicar la rotació al pájaro
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    // Mètode OnCollisionEnter2D: s'executa quan el pájaro col·lisiona amb un altre objecte
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe") || collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("ceiling"))
        {
            GameOver = true;
            gm.GetComponent<GameManager>().GameOver();
            animator.SetTrigger("dead");  // Activar el trigger de l'animació "dead"
            rb.velocity = new Vector2(0, rb.velocity.y);  // Opcional: ajustar la velocitat horitzontal a 0 per a que no es desplaci horitzontalment
            Debug.Log("Col·lisió amb un pipe");
        }
    }

    // Mètode OnTriggerEnter2D: s'executa quan el pájaro entra en un altre collider
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            gm.GetComponent<GameManager>().incrementScore();
            Debug.Log("Goal");
        }
    }
}