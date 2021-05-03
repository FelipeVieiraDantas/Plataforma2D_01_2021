using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public int hp = 1;
    public float velocidadeMachuca = 2;

    public Sprite machucado;

    int maxHP;
    SpriteRenderer spriteRenderer;

    public GameObject explosaoPrefab;

    private void Start()
    {
        maxHP = hp;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D fisica = collision.collider.GetComponent<Rigidbody2D>();
        if (fisica != null)
        {
            if(fisica.velocity.magnitude >= velocidadeMachuca)
            {
                hp = hp - 1;
                if(hp <= 0)
                {
                    Destroy(gameObject);
                    GameObject explosaoCriada = Instantiate(explosaoPrefab, transform.position, transform.rotation);
                    Destroy(explosaoCriada, 1);
                }else if (hp<= maxHP/2) 
                {
                    spriteRenderer.sprite = machucado;
                }
            }

            Debug.Log("Objeto "+collision.collider+" Bateu em mim com velocidade: "+fisica.velocity.magnitude);
        }
    }
}
