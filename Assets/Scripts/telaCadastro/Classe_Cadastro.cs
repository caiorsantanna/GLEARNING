using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Cadastro : MonoBehaviour {

	public InputField txt_codigo, txt_nome, txt_email, txt_cpf;
	public Text txt_statusCodigo, txt_sala, txt_statusInfo;
	Banco_Cadastro cadastro;
	Objeto_Player player;	
	public GameObject pnl_codigo, pnl_cadastroInfo, pnl_cadastroPlayer;

	// -------------------- Verificar o codigo ---------------------
	public void verificaSala(){
		StartCoroutine("selecionaSala");
	}

	IEnumerator selecionaSala(){
		string codigo = txt_codigo.text;
		cadastro = new Banco_Cadastro();

		if(cadastro.verificaCodigo(codigo)){
			txt_statusCodigo.text = "Sala encontrada! Redirecionando...";
			player = new Objeto_Player();
			txt_sala.text = player.NomeSala;
			yield return new WaitForSeconds(3);
			pnl_codigo.SetActive(false);
			pnl_cadastroInfo.SetActive(true);
		}else{
			txt_statusCodigo.text = "Sala não encontrada. Tente novamente.";
		}
	}
	// -------------------- Verificar o codigo ---------------------

	// -------------------- Validar Informações ---------------------
	public void validarInfo(){
		cadastro = new Banco_Cadastro();
		string nome = txt_nome.text;
		string email = txt_email.text;
		string cpf = txt_cpf.text;

		if(cadastro.verificaInfo(nome, email, cpf)){
			pnl_cadastroInfo.SetActive(false);
			pnl_cadastroPlayer.SetActive(true);
		}else{
			txt_statusInfo.text = "EMAIL OU CPF JÁ CADASTRADOS. ENTRE EM CONTATO COM O ADMINISTRADOR.";
		}
	}
	// -------------------- Validar Informações ---------------------
	

	public void retornaTelaLogin(){
		UnityEngine.SceneManagement.SceneManager.LoadScene("telaLogin");
	}
}
