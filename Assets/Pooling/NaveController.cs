using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveController : MonoBehaviour
{
    Rigidbody2D fisica;
    
    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimentoX = Input.GetAxis("Horizontal");
        float movimentoY = Input.GetAxis("Vertical");

        fisica.velocity = new Vector2(movimentoX,movimentoY)*velocidade;

        Debug.Log(Camera.main.WorldToViewportPoint(transform.position));

        float valorX = Mathf.Clamp(transform.position.x,
            Camera.main.ViewportToWorldPoint(Vector3.zero).x,
            Camera.main.ViewportToWorldPoint(Vector3.one).x);

        float valorY = Mathf.Clamp(transform.position.y,
            Camera.main.ViewportToWorldPoint(Vector3.zero).y,
            Camera.main.ViewportToWorldPoint(Vector3.one).y);

        transform.position = new Vector2(valorX, valorY);
    }
}
