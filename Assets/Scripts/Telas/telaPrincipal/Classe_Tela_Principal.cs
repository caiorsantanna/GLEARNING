using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Tela_Principal : MonoBehaviour {

	public GameObject pnl_megacorp;
	Text txt_nome;
	bool megacorp = false;
	
	void Start(){
		Objeto_Player player = new Objeto_Player();

		txt_nome = GameObject.Find("txt_nome").GetComponent<Text>();
		txt_nome.text = player.Nome;
	}

	public void Metodo_Abrir_Fechar_MegaCorp(){
		if(megacorp){
			pnl_megacorp.SetActive(false);
			megacorp = false;
		}else{
			pnl_megacorp.SetActive(true);
			megacorp = true;
		}
	}

	public void Licao1_Atividade1(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("licao_1_1");
	}

    public void Licao2_Atividade1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("licao_2_1");
    }
}
