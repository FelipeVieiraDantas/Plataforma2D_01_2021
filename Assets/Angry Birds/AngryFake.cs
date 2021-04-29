using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryFake : MonoBehaviour
{

    bool estouClicando;
    Rigidbody2D fisica;

    public SpringJoint2D elastico;
    Vector2 velocidadeAntiga;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (elastico.enabled)
        {
            if (fisica.isKinematic == false && fisica.velocity.magnitude < velocidadeAntiga.magnitude)
            {
                elastico.enabled = false;
                fisica.velocity = velocidadeAntiga;
            }
        }

        if (estouClicando)
        {
            Vector2 posicaoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = posicaoMouse;
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
}
