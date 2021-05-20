using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour
{
    public int HP = 5;

    [Header("Gamefeel")]
    public GameObject mascaraBranca;
    public float tempoPisca = 0.1f;

    public void TomarDano(int quantidade)
    {
        HP -= quantidade;
        if(HP <= 0)
        {
            //MORREU!
        }

        if (mascaraBranca != null)
        {
            mascaraBranca.SetActive(true);
            Invoke("DesligaMascara", tempoPisca);
        }
    }

    void DesligaMascara()
    {
        mascaraBranca.SetActive(false);
    }
}
