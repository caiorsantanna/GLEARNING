using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Banco_Reconexao : MonoBehaviour {	
	public GameObject pnl_reconexao, pnl_tudo;		
    
    public void realizarReconexao(){

		/*
			METODO REALIZA CONEXAO
			Metodo responsavel por realizar tentativas de reconexao quando necessário
			Ordem de execução:
			> Desativa a tela e ativa o pop up de reconexão
			> Inicializa variaveis
			> Realizar um for do qual ira executar 5 vezes correspondentes a 5 tentativas
			> 	Resetar o text dos segundos e atualizar o das tentativas
			> 	Iniciar um while correspondente a contagem dos segundos
			>	Tentar reconexao
			>	Caso de certo em alguma vez, parar tudo e voltar o normal
			>	Caso não, printar na tela e tentar de novo
			> Fim do for
			> Verificar se acabou o for e se sim, voltar na tela incial
			Créditos: Caio Roman Sant'anna
		*/
		pnl_tudo.SetActive(false);
		pnl_reconexao.SetActive(true);
		StartCoroutine(reconexao());
	}

	IEnumerator reconexao(){				

		Text txt_tentativas = GameObject.Find("txt_tentativas").GetComponent<Text>();
		Text txt_contador = GameObject.Find("txt_contador").GetComponent<Text>();

		int contador = 10;
		int segundos = 5;
		int segundosAtuais;
		int i;

		const string url = "http://localhost/";

		for(i = 0; i < contador; i++){
			segundosAtuais = segundos;
			txt_tentativas.text = "("+(i+1)+"/"+contador+")";
			txt_contador.text = segundosAtuais.ToString()+" s.";
			
			while(segundosAtuais > 0){							
				txt_contador.text = segundosAtuais.ToString()+" s.";				
				segundosAtuais--;
				yield return new WaitForSeconds(1);
			}				

			WWW www = new WWW(url);
			yield return www;

			if(www.error == null){
				pnl_tudo.SetActive(true);
				pnl_reconexao.SetActive(false);
				yield break;
			}else{
				segundosAtuais = segundos;
				print(www.error);				
			}
		}

		if(i >= 5){
			UnityEngine.SceneManagement.SceneManager.LoadScene("telaLogin");	
		}else{
			print("OCORREU UM ERRO: Banco_Reconexao");
		}

	}	

}