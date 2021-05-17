using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5;
    Rigidbody2D fisica;

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

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        scriptDoDano = GetComponent<Mortal>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bloquear o controle enquanto está dando o dash
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
            }
            return;
        }



        //Movimento lateral
        float inputDoJogador = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(inputDoJogador * velocidade, fisica.velocity.y);

        //Descobrir se está no chão
        Collider2D chao = Physics2D.OverlapCircle(posicaoPe.position, 0.01f, layerDoChao);
        if(chao != null)
        {
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
        }


        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !jaDeuDash)
        {
            fisica.velocity = Vector2.zero;
            fisica.AddForce(new Vector2(inputDoJogador * forcaDash, 0), ForceMode2D.Impulse);
            fisica.gravityScale = 0;
            estouDashando = true;
            jaDeuDash = true;
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

            fisica.AddForce(forcaAdicionada * forcaKnockBack,ForceMode2D.Impulse);
            estouComKnockBack = true;
        }
    }

}
