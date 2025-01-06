using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 5f; // Fuerza que se aplicará al pájaro al tocar la pantalla
    private Rigidbody2D rb;
    public GameObject gm;
    private bool GameOver = false;
    private bool gameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D del pájaro
        gm = GameObject.FindGameObjectWithTag("gm");
        rb.isKinematic = true; // Hacemos que el pájaro no se mueva al inicio
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)))
        {
            gameStarted = true;
            rb.isKinematic = false; // Permitimos que el pájaro se mueva
        }

        if (gameStarted && !GameOver)
        {
            if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.X)) // Si se toca la pantalla
            {
                rb.velocity = Vector2.up * fuerzaSalto; // Aplicamos fuerza hacia arriba al Rigidbody2D
            }

            // Rotación del pájaro
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe") || collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("ceiling"))
        {
            GameOver = true;
            gm.gameObject.GetComponent<GameManager>().GameOver();
            Debug.Log("Colisión con un pipe");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            gm.gameObject.GetComponent<GameManager>().incrementScore();
            Debug.Log("Goal");
        }
    }
}