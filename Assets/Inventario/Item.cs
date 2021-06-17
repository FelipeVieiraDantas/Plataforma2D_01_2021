using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Novo Item",menuName ="Criar Item")]
public class Item : ScriptableObject
{
    public string nome;
    public Sprite icone;

    public int quantidade;
}
