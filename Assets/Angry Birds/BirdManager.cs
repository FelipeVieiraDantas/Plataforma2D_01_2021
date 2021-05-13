using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    AngryFake[] passarosDisponiveis;
    int passaroAtual;

    public static BirdManager singleton;

    public SpringJoint2D elastico;

    public Transform posicaoInicial;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;

        passarosDisponiveis = FindObjectsOfType<AngryFake>();
        VerificaPassaro();
    }

    public void VerificaPassaro()
    {
        if(passaroAtual < passarosDisponiveis.Length)
        {
            //Ainda tem pássaro pra jogar
            elastico.connectedBody = passarosDisponiveis[passaroAtual].GetComponent<Rigidbody2D>();
            elastico.enabled = true;

            passarosDisponiveis[passaroAtual].minhaVez = true;

            passarosDisponiveis[passaroAtual].transform.position = posicaoInicial.position;
            Camera.main.GetComponent<CameraComLimite>().alvo = passarosDisponiveis[passaroAtual].transform;

            passaroAtual++;
        }
        else
        {
            //Reiniciar a fase
        }
    }
}
