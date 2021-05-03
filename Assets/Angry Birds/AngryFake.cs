using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AngryFake : MonoBehaviour
{

    bool estouClicando;
    Rigidbody2D fisica;

    public SpringJoint2D elastico;
    Vector2 velocidadeAntiga;

    public float maxEstica = 2;

    PigController[] porcosNaCena;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        porcosNaCena = FindObjectsOfType<PigController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool existePorcoVivo = false;
        for (int i = 0; i < porcosNaCena.Length; i++)
        {
            if(porcosNaCena[i] != null)
            {
                existePorcoVivo = true;
                break;
            }
        }
        if (!existePorcoVivo)
        {
            ReiniciarFase();
        }

        if (elastico.enabled)
        {
            if (fisica.isKinematic == false && fisica.velocity.magnitude < velocidadeAntiga.magnitude)
            {
                elastico.enabled = false;
                fisica.velocity = velocidadeAntiga;
            }
        }
        else
        {
            if(fisica.velocity.magnitude <= 0.05f)
            {
                ReiniciarFase();
            }
        }

        if (estouClicando)
        {
            Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distanciaMouseEstilingue = Vector2.Distance(posicaoMouse,elastico.transform.position);
            if (distanciaMouseEstilingue <= maxEstica)
            {
                transform.position = posicaoMouse;
            }
            else
            {
                Vector2 posElastico = elastico.transform.position;
                Vector2 direcao = posicaoMouse - posElastico;
                Ray2D linhaProMouse = new Ray2D(elastico.transform.position, direcao);
                transform.position = linhaProMouse.GetPoint(maxEstica);
            }
        }
        else
        {
            velocidadeAntiga = fisica.velocity;
        }


    }

    void OnMouseDown()
    {
        estouClicando = true;
    }

    void OnMouseUp()
    {
        estouClicando = false;
        fisica.isKinematic = false;
    }

    void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReiniciarFase();
    }

}
