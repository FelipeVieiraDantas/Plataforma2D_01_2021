using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    public float velocidade = 10;
    protected Rigidbody2D fisica;

    public Transform[] pontos;
    public int posicaoPontos;
    public float distanciaAceitavel = 0.5f;

    protected SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();

        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Vector2 direcao = pontos[posicaoPontos].position - transform.position;
        direcao.Normalize();

        if (Vector2.Distance(pontos[posicaoPontos].position, transform.position) <= distanciaAceitavel)
        {
            posicaoPontos++;
            //posicaoPontos = posicaoPontos + 1;
            if(posicaoPontos >= pontos.Length)
            {
                posicaoPontos = 0;
            }
        }

        fisica.velocity = direcao * velocidade;


        //Flipar o sprite
        if(direcao.x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
