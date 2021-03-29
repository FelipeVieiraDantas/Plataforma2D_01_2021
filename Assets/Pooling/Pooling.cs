using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject objetoParaInstanciar;
    public int comecaInstanciado = 10;

    public List<GameObject> listaDeObjetos;

    public bool podeAumentar = true;

    // Start is called before the first frame update
    void Start()
    {
        listaDeObjetos = new List<GameObject>();

        //Uma repetição para criar vários objetos em cena
        for (int i = 0; i < comecaInstanciado; i++)
        {
            //Cada objeto criado, vai ficar guardado dentro da variável listaDeObjetos
            listaDeObjetos.Add(Instantiate(objetoParaInstanciar));
            //Vou pegar o objeto que eu acabei de criar e vou fazer ele começar
            //Desativado
            listaDeObjetos[i].SetActive(false);
        }
        
    }

    public GameObject PegaObjeto()
    {
        for (int i = 0; i < listaDeObjetos.Count; i++)
        {
            if (!listaDeObjetos[i].activeInHierarchy)
            {
                return listaDeObjetos[i];
            }
        }

        if (!podeAumentar)
        {
            return null;
        }

        listaDeObjetos.Add(Instantiate(objetoParaInstanciar));
        return listaDeObjetos[listaDeObjetos.Count-1];
    }
}
