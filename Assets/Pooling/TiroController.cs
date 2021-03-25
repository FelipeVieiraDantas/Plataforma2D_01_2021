using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    Rigidbody2D fisica;
    public float velocidade = 15;

    // Start is called before the first frame update
    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        Destroy(gameObject,2);
    }

    // Update is called once per frame
    void Update()
    {
        fisica.velocity = new Vector2(0, velocidade);
    }
}
