using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Banco_Reconexao : MonoBehaviour {
	private const string url = "http://localhost/";
	public GameObject pnl_reconexao, pnl_todo;	
	public Text txt_tentativas, txt_contador;

	private int contador, segundos, segundosAtuais;

    
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

		StartCoroutine(reconexao());
	}

	IEnumerator reconexao(){
		pnl_todo.SetActive(false);
		pnl_reconexao.SetActive(true);

		contador = 10;
		segundos = 5;
		int i;

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
				pnl_todo.SetActive(true);
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
			print("OCORREU UM ERRO LINHA 59: Banco_Reconexao");
		}

	}	

}
