using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodeAtirar : MonoBehaviour
{
    Pooling poolingDoTiro;

    protected void SelecionaTiro(float multiplicador = 1)
    {
        GameObject novotiro = poolingDoTiro.PegaObjeto();
        novotiro.transform.position = transform.position;
        novotiro.SetActive(true);

        TiroController scriptDoTiro = novotiro.GetComponent<TiroController>();
        scriptDoTiro.velocidade = Mathf.Abs(scriptDoTiro.velocidade) * multiplicador;
        scriptDoTiro.quemAtirou = gameObject;
    }
}
