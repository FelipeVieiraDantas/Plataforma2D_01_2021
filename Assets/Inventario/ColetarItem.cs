using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColetarItem : MonoBehaviour
{
    public Item qualItem;
    public int quantidade = 1;

    public void PegueiItem()
    {
        Inventario.instancia.AdicionarItem(qualItem, quantidade);
    }
}
