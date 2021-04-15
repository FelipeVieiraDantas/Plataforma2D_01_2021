using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManController : MonoBehaviour
{
    public float velocidade = 5;
    Rigidbody2D fisica;

    public GameObject prefabDoTiro;

    public Color[] coresPossiveis;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float apertando = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(apertando * velocidade, fisica.velocity.y);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject acabeiDeAtirar = Instantiate(prefabDoTiro, transform.position, transform.rotation);

            //Descobrir um número aleatório dentro de um Range
            float yAleatorio = Random.Range(-5f, 5f);

            //Pegar cor aleatoria
            int corAleatoria = Random.Range(0, coresPossiveis.Length);

            acabeiDeAtirar.GetComponent<SpriteRenderer>().color = coresPossiveis[corAleatoria];
            acabeiDeAtirar.GetComponent<TrailRenderer>().startColor = coresPossiveis[corAleatoria];
            acabeiDeAtirar.GetComponent<TrailRenderer>().endColor = coresPossiveis[corAleatoria];
            

            acabeiDeAtirar.GetComponent<Rigidbody2D>().velocity = new Vector2(50, yAleatorio);
            Destroy(acabeiDeAtirar, 3);
        }
    }
}
