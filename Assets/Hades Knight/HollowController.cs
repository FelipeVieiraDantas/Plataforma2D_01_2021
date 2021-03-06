using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direcao
{
    Direita = 1, Esquerda = -1, Cima = 2, Baixo = -2, Nenhum = 0
}
//INTERVALO! Voltamos 9:55!
public class HollowController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5;
    Rigidbody2D fisica;
    public Direcao ultimaDirHorizontal;
    public Direcao ultimaDirVertical = Direcao.Nenhum;


    [Header("Pulo")]
    public float forcaPulo = 200;
    bool estaNoChao;
    public LayerMask layerDoChao;
    public Transform posicaoPe;
    public int quantidadeDePulos = 1;
    int jaPuleiQuantasVezes;

    [Header("Dash")]
    public float forcaDash = 30;
    bool estouDashando;
    public float tempoDash = 1;
    float contadorDash;
    bool jaDeuDash;

    [Header("Dano")]
    public float knockBackTempo = 0.5f;
    Mortal scriptDoDano;
    float contadorKnocBack;
    bool estouComKnockBack;
    public float forcaKnockBack = 10;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        scriptDoDano = GetComponent<Mortal>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bloquear o controle enquanto est? dando o dash
        if (estouDashando)
        {
            contadorDash += Time.deltaTime;
            if(contadorDash >= tempoDash)
            {
                contadorDash = 0;
                estouDashando = false;
                fisica.gravityScale = 1;
            }
            return;
        }

        //Pegar tempo do Knockback!
        if (estouComKnockBack)
        {
            contadorKnocBack += Time.deltaTime;
            if(contadorKnocBack >= knockBackTempo)
            {
                contadorKnocBack = 0;
                estouComKnockBack = false;
                anim.SetBool("Apanhou", false);
            }
            return;
        }

        if(scriptDoDano.HP <= 0) {
            return;
        }



        //Movimento lateral
        float inputDoJogador = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(inputDoJogador * velocidade, fisica.velocity.y);

        //Descobrir se est? no ch?o
        Collider2D chao = Physics2D.OverlapCircle(posicaoPe.position, 0.01f, layerDoChao);
        if(chao != null)
        {
            if(estaNoChao == false)
            {
                anim.SetTrigger("EncostouNoCh?o");
            }
            estaNoChao = true;
            jaPuleiQuantasVezes = 0;
            jaDeuDash = false;
        }
        else
        {
            estaNoChao = false;
        }
        //Pulo
        if(Input.GetKeyDown(KeyCode.Space) && jaPuleiQuantasVezes < quantidadeDePulos)
        {
            fisica.velocity = Vector2.zero;
            fisica.AddForce(new Vector2(0, forcaPulo));
            jaPuleiQuantasVezes++;
            anim.SetTrigger("Pulou");
        }


        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !jaDeuDash)
        {
            fisica.velocity = Vector2.zero;
            fisica.AddForce(new Vector2((int)ultimaDirHorizontal * forcaDash, 0), ForceMode2D.Impulse);
            fisica.gravityScale = 0;
            estouDashando = true;
            jaDeuDash = true;
        }


        //Setar ?ltimos valores apertados
        if(inputDoJogador < 0)
        {
            ultimaDirHorizontal = Direcao.Esquerda;
        }else if(inputDoJogador > 0)
        {
            ultimaDirHorizontal = Direcao.Direita;
        }
        float inputVertical = Input.GetAxis("Vertical");
        if(inputVertical < 0)
        {
            ultimaDirVertical = Direcao.Baixo;
        }else if(inputVertical > 0)
        {
            ultimaDirVertical = Direcao.Cima;
        }
        else
        {
            ultimaDirVertical = Direcao.Nenhum;
        }


        //Colocar o input na anima??o
        anim.SetFloat("InputVertical", inputVertical);
        anim.SetFloat("InputHorizontal", Mathf.Abs(inputDoJogador));

        //Flipar sprite
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if(inputDoJogador < 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }else if(inputDoJogador > 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }

        //Verificar se ele est? caindo
        if(fisica.velocity.y < 0)
        {
            anim.SetTrigger("Caindo");
            /*Quem quiser usar um hash ao inv?s de pegar por string
             * ? mais otimizado. Nesse caso, criaria a vari?vel do hash no Start
             * E aplicaria da forma abaixo
             * */
            /*int caindo = Animator.StringToHash("Caindo");
            anim.SetTrigger(caindo);*/
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Bati num inimigo?
        if (collision.collider.CompareTag("Finish"))
        {
            scriptDoDano.TomarDano(1);
            Vector2 direcao = collision.transform.position - transform.position;
            direcao.Normalize();

            Vector2 forcaAdicionada = -direcao;
            forcaAdicionada.y = 1;

            //Cancelar dash se tomar dano
            fisica.velocity = Vector2.zero;
            jaDeuDash = false;
            fisica.gravityScale = 1;

            fisica.AddForce(forcaAdicionada * forcaKnockBack,ForceMode2D.Impulse);
            estouComKnockBack = true;

            anim.SetBool("Apanhou", true);
        }
    }

}
