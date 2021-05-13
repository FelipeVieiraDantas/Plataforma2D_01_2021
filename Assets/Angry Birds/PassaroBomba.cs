using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassaroBomba : AngryFake
{
    public float raioExplosao = 2;
    public float forcaExplosao = 10;
    bool jaExplodi;

    protected override void Update()
    {
        base.Update();

        if(minhaVez && !jaExplodi && !elastico.enabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                jaExplodi = true;
                gameObject.SetActive(false);

                //Chamar função que procura próximo na fila
                ReiniciarFase();

                Collider2D[] bati = Physics2D.OverlapCircleAll(transform.position, raioExplosao);
                foreach(Collider2D objeto in bati)
                {
                    Rigidbody2D fisica = objeto.GetComponent<Rigidbody2D>();
                    if(fisica != null)
                    {
                        Vector2 direcao = objeto.transform.position - transform.position;
                        direcao.Normalize();
                        fisica.AddForce(direcao * forcaExplosao, ForceMode2D.Impulse);
                    }
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raioExplosao);
    }

}
