using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBulletScript : MonoBehaviour
{
    public GameObject prefabDaExplosao;

    public Vector2 forcaExplosao;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            GameObject explosao = Instantiate(prefabDaExplosao, transform.position, transform.rotation);
            Destroy(explosao, 1);
            Destroy(gameObject);

            collision.GetComponent<Rigidbody2D>().AddForce(forcaExplosao, ForceMode2D.Impulse);
        }
    }
}
