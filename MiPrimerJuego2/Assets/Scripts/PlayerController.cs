using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour
{
    //Movimiento
    public float velocidad;

    public Animator animator;


    //Salto
    public float fuerzaSalto = 10f;
    public float longitud = 0.1f;

    public LayerMask capSuelo;

    private bool enSuelo;

    private Rigidbody2D rb;

    //Daño

    private bool reciveDanio;
    public float fuerzaRebote = 5f;

    //Vida
    public int vida = 3;

    public bool muerto;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            Movement();
            Jump();

        }
        animator.SetBool("ifSuelo", enSuelo);
        animator.SetBool("danioR", reciveDanio);
        animator.SetBool("muerto", muerto);

    }

    public void Movement()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("Correr", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 posicion = transform.position;
        if(!reciveDanio)
        {
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
        }
    }

    public void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,longitud,capSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);

        }
    }

    public void RecibiendoDanio(Vector2 direccion, int cantDanio)
    {
        if (!reciveDanio)
        {
            reciveDanio = true;
            vida -= cantDanio;

            if (vida <= 0)
            {
                muerto = true;
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }

        }
    }

    public void DesactivaDanio()
    {
        reciveDanio = false;
        rb.velocity = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitud);
    }
}
