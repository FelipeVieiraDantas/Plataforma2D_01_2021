using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;

    public int quantidadeSlots;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < quantidadeSlots; i++)
        {
            GameObject acabeiDeInstanciar = Instantiate(slotPrefab, transform);

            int numeroAUsar = i;
            acabeiDeInstanciar.GetComponent<Button>().onClick.AddListener(() => UsarItem(numeroAUsar));
        }
    }

    public void UsarItem(int qualSlot)
    {
        Debug.Log("Usei o item: "+qualSlot);

        string soNumero = transform.GetChild(qualSlot).GetChild(0).GetChild(0).GetComponent<Text>().text;
        soNumero = soNumero.Substring(1);

        int quantidade = int.Parse(soNumero);
        if (quantidade > 0)
        {
            quantidade--;
            transform.GetChild(qualSlot).GetChild(0).GetChild(0).GetComponent<Text>().text = "x" + quantidade.ToString();
        }

        //INTERVALO!!! Voltamos 9:55
    }

}
