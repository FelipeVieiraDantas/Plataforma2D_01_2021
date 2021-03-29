using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveAtira : MonoBehaviour
{
    public Pooling poolingDoTiro;

    public float cadencia = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Atirar", 0,cadencia);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke();
        }
    }

    void Atirar()
    {
        GameObject novoTiro = poolingDoTiro.PegaObjeto();
        if(novoTiro != null)
        {
            novoTiro.transform.position = transform.position;
            novoTiro.SetActive(true);
        }
    }
}
