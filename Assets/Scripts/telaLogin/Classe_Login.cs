using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_Login : MonoBehaviour {

	public InputField txtLogin, txtSenha;
	public GameObject pnl_login, pnl_continuar, pnl_todo, pnl_reconexao;	
	public Text txtTituloContinuar, txtContinuar;
	Banco_Login login;
	Objeto_Player jogador;

	public void logar(){
		string sucesso = "Sucesso!";
		string falha = "Login Inválido...";
		string erro = "Ocorreu um erro.";		

		login = new Banco_Login();		

		switch(login.retornaLogin(txtLogin.text, txtSenha.text)){
			case "ESTUDANTE":
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = sucesso;
				txtContinuar.text = "Entrando como Estudante, aperte em continuar!";
				break;
			case "PROFESSOR":
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = sucesso;
				txtContinuar.text = "Entrando como Professor, aperte em continuar!";
				break;
			case "MONITOR":
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = sucesso;
				txtContinuar.text = "Entrando como Monitor, aperte em continuar!";
				break;
			case "ADM":
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = sucesso;
				txtContinuar.text = "Entrando como Administrador, aperte em continuar!";
				break;
			case "FALSO":
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = falha;
				txtContinuar.text = "Login ou Senha Inválidos! Tente novamente.";
				break;
			default:
				pnl_login.SetActive(false);
				pnl_continuar.SetActive(true);
				txtTituloContinuar.text = erro;
				txtContinuar.text = "Ocorreu um erro :/. Por favor, contate o administrador.\ncaiorsantanna@gmail.com";
				break;
		}

	}

	public void continuar(){
		jogador = new Objeto_Player();
		print(jogador.TipoLogin);
		switch(jogador.TipoLogin){
			case "ESTUDANTE":

				break;
			case "PROFESSOR":

				break;
			case "MONITOR":

				break;
			case "ADM":

				break;
			default:
				pnl_login.SetActive(true);
				pnl_continuar.SetActive(false);
				break;
		}
	}

}
