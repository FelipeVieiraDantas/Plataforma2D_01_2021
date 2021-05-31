using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowCollider : MonoBehaviour
{
    public RuntimeAnimatorController trocarPara;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Animator>().runtimeAnimatorController = trocarPara;
    }
}
