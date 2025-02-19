using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;

    public float detectionRadius = 5.0f;

    public float speed = 2.0f;

    private Rigidbody2D rb;

    private Vector2 movement;

    public Animator animator;
    public bool moveAn;


    private bool playerLife;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerLife = true;
    }

    void Update()
    {

        animator.SetBool("Mov", moveAn);
        if (playerLife)
        {
            Movimiento();
        }
        else
        {
            moveAn = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            PlayerController playerS = collision.gameObject.GetComponent<PlayerController>();
            playerS.RecibiendoDanio(direccionDanio, 1);
            playerLife = !playerS.muerto;

        }
    }
    private void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position -transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            moveAn = true;

        }

        else
        {
            movement = Vector2.zero;
            moveAn = false;
        }

        rb.MovePosition(rb.position + movement*speed*Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
