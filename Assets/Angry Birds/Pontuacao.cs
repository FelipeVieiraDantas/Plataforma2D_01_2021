using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pontuacao : MonoBehaviour
{
    public int pontuacao;
    public Text texto;

    public static Pontuacao singleton;

    void Start()
    {
        singleton = this;

        if (PlayerPrefs.HasKey("Pontuação"+SceneManager.GetActiveScene().buildIndex.ToString()))
        {
            texto.text = PlayerPrefs.GetInt("Pontuação" + SceneManager.GetActiveScene().buildIndex.ToString()).ToString();
        }
    }

    public void AdicionaPonto()
    {
        pontuacao++;
        texto.text = pontuacao.ToString();

        PlayerPrefs.SetInt("Pontuação" + SceneManager.GetActiveScene().buildIndex.ToString(), pontuacao);
        PlayerPrefs.Save();
    }

    public void ResetarSave()
    {
        PlayerPrefs.DeleteKey("Pontuação" + SceneManager.GetActiveScene().buildIndex.ToString());
        texto.text = "0";
    }

    public void CarregarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
