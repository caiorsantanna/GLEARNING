using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Tela_Principal : MonoBehaviour {

	public GameObject pnl_megacorp;
	Text txt_nome;
    public Image img_base, img_cabelo, img_roupa;
	
	void Start(){
		Objeto_Player player = new Objeto_Player();

		txt_nome = GameObject.Find("txt_nome").GetComponent<Text>();
		txt_nome.text = player.Nome;

        img_base.sprite = Resources.LoadAll<Sprite>("Sprites/"+ player.Pele)[1];
        img_cabelo.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Cabelo)[1];
        img_roupa.sprite = Resources.LoadAll<Sprite>("Sprites/" + player.Roupa)[1];
    }	

	public void Licao1_Atividade1(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_1_1");
	}

    public void Licao2_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_2_1");
    }

    public void Licao3_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_3_1");
    }

    public void Licao4_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_4_1");
    }

    public void Licao5_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_5_1");
    }

    public void Licao6_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_6_1");
    }
}
