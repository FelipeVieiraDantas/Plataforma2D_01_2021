using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbelhaController : InimigoController
{
    [Header("A partir daqui são variáveis do Abelha Controller")]
    public Transform alvo;

    public override void Update()
    {
        if (alvo == null)
        {
            base.Update();
        }
        else
        {
            Vector2 direcao = alvo.position - transform.position;
            direcao.Normalize();

            fisica.velocity = direcao * velocidade;

            //Flipar o sprite
            if (direcao.x > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            alvo = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            alvo = null;
        }
    }
}
