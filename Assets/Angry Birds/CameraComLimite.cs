using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComLimite : MonoBehaviour
{
    public Transform alvo;
    public Transform posicaoMin, posicaMax;

    // Update is called once per frame
    void Update()
    {
        Vector3 novaPosicao = alvo.position;
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, posicaoMin.position.x, posicaMax.position.x);
        novaPosicao.y = Mathf.Clamp(novaPosicao.y, posicaoMin.position.y, posicaMax.position.y);
        novaPosicao.z = -10;

        transform.position = novaPosicao;
    }
}
