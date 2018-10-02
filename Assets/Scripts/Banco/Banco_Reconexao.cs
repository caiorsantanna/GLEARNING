using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

public class Banco_Reconexao : MonoBehaviour {
	private const string url = "http://localhost/";
	public GameObject pnl_reconexao, pnl_todo, img_reconexao, btn_reconectar;	
	public Text txt_tentativas, txt_contador;

	private int contador, segundos, segundosAtuais;
	private bool conexao = false;

    
    public void reconexaoSelect(){
		btn_reconectar.SetActive(false);
		img_reconexao.SetActive(true);

		contador = 5;
		segundos = 5;
		segundosAtuais = segundos;

		int i = 0;
		StartCoroutine(tempo());

		while(i < contador){
			segundosAtuais = segundos;
			txt_tentativas.text = "("+(i+1)+"/"+contador+")";
			txt_contador.text = segundosAtuais.ToString()+" s.";
			print("dentro do for antes tempo"+i);
			
			print("dentro do for dps do tempo"+i);	

			WWW www = new WWW(url);
			StartCoroutine(testaConexao(www));

			StopAllCoroutines();

			if(conexao){
				pnl_todo.SetActive(true);
				pnl_reconexao.SetActive(false);
				return;
			}else{
				segundosAtuais = segundos;
			}

			contador++;											
		}

		img_reconexao.SetActive(false);
		btn_reconectar.SetActive(true);
		
		UnityEngine.SceneManagement.SceneManager.LoadScene("telaLogin");
	}

	IEnumerator tempo(){
		
		//while(segundosAtuais > 0){
			print("dentro tempo antes "+segundosAtuais);
			yield return new WaitForSeconds(1);
			print("dentro tempo depois "+segundosAtuais);
			txt_contador.text = segundosAtuais.ToString()+" s.";				
			segundosAtuais--;
		//}		
	}

	private IEnumerator testaConexao(WWW con){				
		yield return con;

		if(con.error == null){
			conexao = true;		
		}else{
			conexao = false;
			print(con.error);
		}
	}

}
