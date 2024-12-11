using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    private float fuerzaSalto = 5f; // Fuerza que se aplicará al pájaro al tocar la pantalla
    private Rigidbody2D rb;
    public GameObject gm;
    private bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtenemos el componente Rigidbody2D del pájaro
        gm = GameObject.FindGameObjectWithTag("gm");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !GameOver) // Si se toca la pantalla
        {
            rb.velocity = Vector2.up * fuerzaSalto; // Aplicamos fuerza hacia arriba al Rigidbody2D
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("pipe"))
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
