using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveDoMau : MonoBehaviour
{
    public float velocidade = 10;

    public Pooling poolingDoTiro;

    float tempoDeTiro;

    public LayerMask layerDoJogador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocidade * Time.deltaTime, 0, 0);

        if(Camera.main.WorldToViewportPoint(transform.position).x < 0 ||
            Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            velocidade = velocidade * -1;
            //velocidade *= -1;
        }

        if(tempoDeTiro > 0)
        {
            tempoDeTiro -= Time.deltaTime;
            //tempoDeTiro = tempoDeTiro - Time.deltaTime;
            return;
        }

        RaycastHit2D atingiu = Physics2D.Raycast(transform.position, Vector2.down,10,layerDoJogador);
        if(atingiu.collider != null)
        {
            GameObject novotiro = poolingDoTiro.PegaObjeto();
            novotiro.transform.position = transform.position;
            novotiro.SetActive(true);

            TiroController scriptDoTiro = novotiro.GetComponent<TiroController>();
            scriptDoTiro.velocidade = Mathf.Abs(scriptDoTiro.velocidade) * -1;
            scriptDoTiro.quemAtirou = gameObject;

            tempoDeTiro = 0.5f;
        }
        Debug.DrawRay(transform.position, Vector2.down *10, Color.red);
    }
}
