using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajetoria : MonoBehaviour
{
    public Rigidbody2D passaroFalso;
    public SpringJoint2D elastico;

    SpringJoint2D elasticoFalso;
    Vector2 velocidadeAntiga;

    Vector3 ultimaPosicao;

    [Header("Visual da Trajetória")]
    [Tooltip("Coloque abaixo o prefab da bolinha")]
    public GameObject bolinhaPrefab;
    public int quantidadeBolas = 10;
    public float distanciaTempo = 0.25f;
    List<GameObject> bolinhasCriadas;
    int ultimaBola;

    // Start is called before the first frame update
    void Start()
    {
        //Criar um elástico falso
        elasticoFalso = elastico.gameObject.AddComponent<SpringJoint2D>();
        elasticoFalso.connectedBody = passaroFalso;
        elasticoFalso.anchor = elastico.anchor;
        elasticoFalso.autoConfigureDistance = elastico.autoConfigureDistance;
        elasticoFalso.distance = elastico.distance;

        //Criar as bolinhas da trajetória
        bolinhasCriadas = new List<GameObject>();
        for (int i = 0; i < quantidadeBolas; i++)
        {
            GameObject bola = Instantiate(bolinhaPrefab);
            bolinhasCriadas.Add(bola);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se o pássaro real se moveu, e então recalcular a rota
        if(transform.position != ultimaPosicao)
        {
            ultimaPosicao = transform.position;
            SeMoveu();
        }

        //Cortar o elástico falso quando a velocidade máxima for atingida
        if(elasticoFalso.enabled && velocidadeAntiga.magnitude > passaroFalso.velocity.magnitude)
        {
            elasticoFalso.enabled = false;
            passaroFalso.velocity = velocidadeAntiga;
        }
        else
        {
            velocidadeAntiga = passaroFalso.velocity;
        }
    }

    void SeMoveu()
    {
        passaroFalso.velocity = Vector2.zero;
        passaroFalso.transform.position = transform.position;
        velocidadeAntiga = Vector2.zero;
        elasticoFalso.enabled = true;

        ultimaBola = 0;
        CancelInvoke();
        Invoke("MoveBolinha", distanciaTempo);
    }

    void MoveBolinha()
    {
        Color novaCor = Color.white;
        novaCor.a = Mathf.Lerp(1,0, (float)ultimaBola / (float)quantidadeBolas);
        bolinhasCriadas[ultimaBola].GetComponent<SpriteRenderer>().color = novaCor;

        bolinhasCriadas[ultimaBola].transform.position = passaroFalso.transform.position;
        ultimaBola = ultimaBola + 1;
        if(ultimaBola < quantidadeBolas)
        {
            Invoke("MoveBolinha", distanciaTempo);
        }
    }
}
