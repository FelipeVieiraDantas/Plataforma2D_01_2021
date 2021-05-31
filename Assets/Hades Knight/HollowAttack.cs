using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowAttack : MonoBehaviour
{
    public Transform posAtaque_esquerda, posAtaque_direita, posAtaque_cima, posAtaque_baixo;
    public float raioAtaque = 0.5f;
    public int quantidadeDeDano = 1;
    HollowController hollowController;

    [Header("Knockback Inimigo")]
    public float forcaKnockbackInimigo = 10;

    [Header("Tempos de ataque")]
    public float tempoDeAtaque = 0.5f;
    float contadorTempoAtaque;

    [Header("Efeitos")]
    public ParticleSystem efeitoAtaque;


    bool atacando;
    bool deuHit;

    // Start is called before the first frame update
    void Start()
    {
        hollowController = GetComponent<HollowController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(contadorTempoAtaque > 0)
        {
            contadorTempoAtaque -= Time.deltaTime;
            return;
        }

        if (atacando)
        {
            Animator anim = GetComponent<Animator>();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Ataque"))
            {
                atacando = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
        {
            if (!atacando)
            {
                //Animar o ataque
                GetComponent<Animator>().SetTrigger("Atacou");
                atacando = true;
            }else if (deuHit)
            {
                GetComponent<Animator>().SetTrigger("Combo");
                deuHit = false;
            }

        }

        

    }

    public void AtaqueDeFato()
    {
        deuHit = true;


        //Descobrir qual direção é pra atacar
        Transform posAtaque = posAtaque_direita;
        if (hollowController.ultimaDirHorizontal == Direcao.Esquerda)
        {
            posAtaque = posAtaque_esquerda;
        }
        if (hollowController.ultimaDirVertical != Direcao.Nenhum)
        {
            posAtaque = posAtaque_cima;
            if (hollowController.ultimaDirVertical == Direcao.Baixo)
            {
                posAtaque = posAtaque_baixo;
            }
        }


        //Colocar a partícula na posição do ataque
        efeitoAtaque.transform.position = posAtaque.position;
        efeitoAtaque.transform.rotation = posAtaque.rotation;
        //Resetar a partícula se já estiver tocando
        efeitoAtaque.Clear();
        //Tocar a partícula
        efeitoAtaque.Play();

        Collider2D[] bati = Physics2D.OverlapCircleAll(posAtaque.position, raioAtaque);
        foreach (Collider2D apanhou in bati)
        {
            if (apanhou.gameObject != gameObject)
            {
                Mortal scriptDeDano = apanhou.GetComponent<Mortal>();
                if (scriptDeDano != null)
                {
                    contadorTempoAtaque = tempoDeAtaque;

                    scriptDeDano.TomarDano(quantidadeDeDano);

                    Rigidbody2D fisicaInimigo = apanhou.GetComponent<Rigidbody2D>();
                    if (fisicaInimigo != null)
                    {
                        Vector2 direcao = apanhou.transform.position - transform.position;
                        direcao.Normalize();
                        direcao.y = 1;
                        fisicaInimigo.AddForce(direcao * forcaKnockbackInimigo, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(posAtaque_direita.position, raioAtaque);
        Gizmos.DrawWireSphere(posAtaque_esquerda.position, raioAtaque);
        Gizmos.DrawWireSphere(posAtaque_cima.position, raioAtaque);
        Gizmos.DrawWireSphere(posAtaque_baixo.position, raioAtaque);
    }

}
