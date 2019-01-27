using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Chefe_Licao1_Atv1 : MonoBehaviour {

	public GameObject canvas_botao, pnl_dialogo, pnl_fechar, pnl_relatorio, pnl_respostas, pnl_acertos, pnl_1, pnl_2, pnl_3, pnl_4, pnl_5, pnl_6;
	public Text txt_dialogo;
	public Dropdown dpwNome_1, dpwNome_2, dpwNome_3, dpwNome_4, dpwNome_5, dpwNome_6;
	public Dropdown dpwSexo_1, dpwSexo_2, dpwSexo_3, dpwSexo_4, dpwSexo_5, dpwSexo_6;
	public Dropdown dpwNascionalidade_1, dpwNascionalidade_2, dpwNascionalidade_3, dpwNascionalidade_4, dpwNascionalidade_5, dpwNascionalidade_6;
	public Image img_estrela_1, img_estrela_2, img_estrela_3;
	public Sprite estrela_sim, estrela_nao;

	public int acertos = 0;

	void Start () {
		Objeto_Player player = new Objeto_Player();
		Classe_Controle_Licao1_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao1_Atv1>();

		pnl_dialogo.SetActive(true);
		pnl_fechar.SetActive(true);		
		txt_dialogo.text = "Good Morning "+player.Nome+", could you give me a report with the names, genders, and nationalities of my employees? Thank you!";

		switch(controle.nivel){
			case 1:
				pnl_1.SetActive(true);
				pnl_2.SetActive(true);
				break;
			case 2:
				pnl_1.SetActive(true);
				pnl_2.SetActive(true);
				pnl_3.SetActive(true);
				pnl_4.SetActive(true);
				break;
			case 3:
				pnl_1.SetActive(true);
				pnl_2.SetActive(true);
				pnl_3.SetActive(true);
				pnl_4.SetActive(true);
				pnl_5.SetActive(true);
				pnl_6.SetActive(true);
				break;
			default:
				print("ERRO");
				break;
		}
		
	}

	public void Metodo_Dialogo_Chefe(){
		pnl_dialogo.SetActive(true);
		pnl_fechar.SetActive(true);
		txt_dialogo.gameObject.SetActive(false);
		pnl_respostas.SetActive(true);
	}

	public void Metodo_Terminar_Licao1_Atv1(){				
		Classe_Controle_Licao1_Atv1 controle = GameObject.Find("scripts").GetComponent<Classe_Controle_Licao1_Atv1>();

		List<Dropdown> dpwNome = new List<Dropdown>();
		dpwNome.Add(dpwNome_1);
		dpwNome.Add(dpwNome_2);
		dpwNome.Add(dpwNome_3);
		dpwNome.Add(dpwNome_4);
		dpwNome.Add(dpwNome_5);
		dpwNome.Add(dpwNome_6);

		List<Dropdown> dpwSexo = new List<Dropdown>();
		dpwSexo.Add(dpwSexo_1);
		dpwSexo.Add(dpwSexo_2);
		dpwSexo.Add(dpwSexo_3);
		dpwSexo.Add(dpwSexo_4);
		dpwSexo.Add(dpwSexo_5);
		dpwSexo.Add(dpwSexo_6);

		List<Dropdown> dpwNascionalidade = new List<Dropdown>();
		dpwNascionalidade.Add(dpwNascionalidade_1);
		dpwNascionalidade.Add(dpwNascionalidade_2);
		dpwNascionalidade.Add(dpwNascionalidade_3);
		dpwNascionalidade.Add(dpwNascionalidade_4);
		dpwNascionalidade.Add(dpwNascionalidade_5);
		dpwNascionalidade.Add(dpwNascionalidade_6);

		for(int i = 0; i <  controle.nivel*2; i++){
			GameObject objNpc = controle.npcs[i].gameObject;
			Classe_NPC_Licao1_Atv1 npc = objNpc.GetComponent<Classe_NPC_Licao1_Atv1>();

			if(npc.nome.Equals(dpwNome[i].captionText.text)){
				acertos++;
			}
			if(npc.sexo.Equals(dpwSexo[i].captionText.text)){
				acertos++;
			}
			if(npc.nascionalidade.Equals(dpwNascionalidade[i].captionText.text)){
				acertos++;
			}
		}
		decimal porcentagem_acertos = (acertos*100)/((controle.nivel*2)*3);
		if(porcentagem_acertos < 50){
			img_estrela_1.sprite = estrela_nao;
			img_estrela_2.sprite = estrela_nao;
			img_estrela_3.sprite = estrela_nao;
		}else if(porcentagem_acertos < 75){
			img_estrela_1.sprite = estrela_sim;
			img_estrela_2.sprite = estrela_nao;
			img_estrela_3.sprite = estrela_nao;
		}else if(porcentagem_acertos < 100){
			img_estrela_1.sprite = estrela_sim;
			img_estrela_2.sprite = estrela_sim;
			img_estrela_3.sprite = estrela_nao;
		}else{
			img_estrela_1.sprite = estrela_sim;
			img_estrela_2.sprite = estrela_sim;
			img_estrela_3.sprite = estrela_sim;
			controle.Metodo_Adicionar_Pontos_Licao1_Atv1();
		}

		pnl_acertos.SetActive(true);
		pnl_relatorio.SetActive(false);
		 
	}

	public void Metodo_Voltar(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
	}

	void OnTriggerEnter2D(){
		canvas_botao.SetActive(true);
	}

	void OnTriggerExit2D(){
		canvas_botao.SetActive(false);
	}
}
