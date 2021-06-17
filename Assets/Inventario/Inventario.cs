using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    public GameObject slotPrefab;

    public int quantidadeSlots;

    public List<Item> itens = new List<Item>();

    public static Inventario instancia;

    // Start is called before the first frame update
    void Start()
    {
        instancia = this;

        for (int i = 0; i < quantidadeSlots; i++)
        {
            GameObject acabeiDeInstanciar = Instantiate(slotPrefab, transform);

            int numeroAUsar = i;
            acabeiDeInstanciar.GetComponent<Button>().onClick.AddListener(() => UsarItem(numeroAUsar));

            if(itens.Count > i)
            {
                acabeiDeInstanciar.transform.GetChild(0).GetComponent<Image>().sprite = itens[i].icone;
                acabeiDeInstanciar.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
                acabeiDeInstanciar.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "x" + itens[i].quantidade;
            }
            else
            {
                acabeiDeInstanciar.transform.GetChild(0).gameObject.SetActive(false);
                acabeiDeInstanciar.GetComponent<Button>().interactable = false;
            }
        }
    }

    public void UsarItem(int qualSlot)
    {
        if(itens.Count <= qualSlot)
        {
            return;
        }

        Debug.Log("Usei o item: "+itens[qualSlot].nome);

        //Pegava o texto e transformana num int. Não precisa mais depois do SO
        /*string soNumero = transform.GetChild(qualSlot).GetChild(0).GetChild(0).GetComponent<Text>().text;
        soNumero = soNumero.Substring(1);

        int quantidade = int.Parse(soNumero);*/
        if (itens[qualSlot].quantidade > 0)
        {
            itens[qualSlot].quantidade--;
            transform.GetChild(qualSlot).GetChild(0).GetChild(0).GetComponent<Text>().text = "x" + itens[qualSlot].quantidade.ToString();

            if(itens[qualSlot].quantidade == 0)
            {
                transform.GetChild(qualSlot).transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(qualSlot).GetComponent<Button>().interactable = false;
                itens.RemoveAt(qualSlot);

                StartCoroutine(ReorganizaLista(qualSlot));
            }
        }
    }

    IEnumerator ReorganizaLista(int qualSlot)
    {
        Transform slotParaMudar = transform.GetChild(qualSlot);
        slotParaMudar.SetParent(null);

        yield return new WaitForEndOfFrame();

        slotParaMudar.SetParent(transform);

        for (int i = 0; i < transform.childCount; i++)
        {
            int numeroAUsar = i;
            transform.GetChild(i).GetComponent<Button>().onClick.RemoveListener(() => UsarItem(numeroAUsar+1));
            transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => UsarItem(numeroAUsar));
        }
    }

    public void AdicionarItem(Item qualItem, int qualQuantidade)
    {
        if (itens.Contains(qualItem))
        {
            int numeroNaLista = itens.IndexOf(qualItem);
            qualItem.quantidade += qualQuantidade;
            transform.GetChild(numeroNaLista).GetChild(0).GetChild(0).GetComponent<Text>().text =
                "x" + qualItem.quantidade;
        }
        else
        {
            itens.Add(qualItem);
            int numeroNaLista = itens.Count - 1;

            qualItem.quantidade = qualQuantidade;

            transform.GetChild(numeroNaLista).transform.GetChild(0).GetComponent<Image>().sprite = itens[numeroNaLista].icone;
            transform.GetChild(numeroNaLista).transform.GetChild(0).GetComponent<Image>().SetNativeSize();
            transform.GetChild(numeroNaLista).transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "x" + itens[numeroNaLista].quantidade;

            transform.GetChild(numeroNaLista).transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(numeroNaLista).GetComponent<Button>().interactable = true;
        }
        
    }

    public bool TemItem(Item qualItem, int quantidade)
    {
        if(itens.Contains(qualItem) && qualItem.quantidade >= quantidade)
        {
            return true;
        }

        return false;
    }

}
