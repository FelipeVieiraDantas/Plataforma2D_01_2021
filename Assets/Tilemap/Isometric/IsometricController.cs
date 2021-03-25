using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IsometricController : MonoBehaviour
{
    public float velocidade = 10;
    Vector3 destino;

    public Tilemap mapa;

    // Start is called before the first frame update
    void Start()
    {
        destino = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 posicaoMouse = Input.mousePosition;
            posicaoMouse = Camera.main.ScreenToWorldPoint(posicaoMouse);

            //Existe um tile onde ele clicou?
            Vector3Int celulaClicada = mapa.WorldToCell(posicaoMouse);
            if (mapa.HasTile(celulaClicada))
            {
                destino = posicaoMouse;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

    }


}
