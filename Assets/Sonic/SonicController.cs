using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicController : MonoBehaviour
{
    public float velocidade = 10;
    Rigidbody2D fisica;
    public float forcaPulo = 200;

    Animator anim;
    SpriteRenderer sprite;

    //Desafio para o homing attack!
    public Transform inimigo;
    public float forcaAtaque = 100;
    bool fazendoAtaque;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float controle = Input.GetAxis("Horizontal");
        //Se o jogador tiver apertando pra esquerda, vai ser -1.
        //Se o jogador tiver apertando para a direita, vai ser 1.
        //Se o jogador não tiver apertando nada, vai ser 0.
        Debug.Log("O valor de controle é: "+controle);

        //Colocamos o if para impedir o movimento enquanto o ataque é feito
        if (fazendoAtaque == false)
        {
            fisica.velocity = new Vector2(velocidade * controle, fisica.velocity.y); //valores de x , y
        }

        //Eu só quero que ele pule se o jogador apertar espaço e ele estiver no chão
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (anim.GetBool("Está no Chão") == true)
            {
                fisica.AddForce(new Vector2(0, forcaPulo));
                anim.SetTrigger("Pulo");
                anim.SetBool("Está no Chão", false);
            }
            else
            {
                //Fazer o homing attack
                Vector2 direcao = inimigo.position - transform.position;
                fisica.AddForce(direcao * forcaAtaque);
                fazendoAtaque = true;
            }
        }

        //Fazer a animação de andar:
        if(controle == 0)
        {
            anim.SetBool("Andando", false);
        }
        else
        {
            anim.SetBool("Andando", true);
            if(controle < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("Está no Chão", true);
        fazendoAtaque = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetBool("Está no Chão", false);
    }

}
