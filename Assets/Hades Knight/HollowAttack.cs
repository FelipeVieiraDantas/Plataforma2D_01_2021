using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowAttack : MonoBehaviour
{
    public Transform posAtaque;
    public float raioAtaque = 0.5f;
    public int quantidadeDeDano = 1;

    [Header("Knockback Inimigo")]
    public float forcaKnockbackInimigo = 10;

    [Header("Tempos de ataque")]
    public float tempoDeAtaque = 0.5f;
    float contadorTempoAtaque;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(contadorTempoAtaque > 0)
        {
            contadorTempoAtaque -= Time.deltaTime;
            return;
        }


        if(Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
        {
            Collider2D[] bati = Physics2D.OverlapCircleAll(posAtaque.position, raioAtaque);
            foreach(Collider2D apanhou in bati)
            {
                if(apanhou.gameObject != gameObject)
                {
                    Mortal scriptDeDano = apanhou.GetComponent<Mortal>();
                    if(scriptDeDano != null)
                    {
                        contadorTempoAtaque = tempoDeAtaque;

                        scriptDeDano.TomarDano(quantidadeDeDano);

                        Rigidbody2D fisicaInimigo = apanhou.GetComponent<Rigidbody2D>();
                        if(fisicaInimigo != null)
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(posAtaque.position, raioAtaque);
    }

}
