using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    Rigidbody2D fisica;
    public float velocidade = 15;

    public GameObject quemAtirou;

    // Start is called before the first frame update
    void OnEnable()
    {
        fisica = GetComponent<Rigidbody2D>();
        Invoke(nameof(Desativar), 2);
    }
    void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        fisica.velocity = new Vector2(0, velocidade);
    }

    void Desativar()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (quemAtirou != collision.gameObject && !collision.gameObject.CompareTag("Finish"))
        {
            Destroy(collision.gameObject);
        }
    }
}
