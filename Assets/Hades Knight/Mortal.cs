using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour
{
    public int HP = 5;

    public void TomarDano(int quantidade)
    {
        HP -= quantidade;
        if(HP <= 0)
        {
            //MORREU!
        }
    }
}
