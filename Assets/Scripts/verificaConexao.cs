using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class verificaConexao : MonoBehaviour {	

	private const string url = "http://localhost/";
	public GameObject pnl_erro, pnl_todo;	
	public Button btn_reconectar;

	public void testaConexao(){
		WWW www = new WWW(url);
		StartCoroutine(executaConexao(www));
	}
	private IEnumerator executaConexao(WWW con){		
		btn_reconectar.interactable = false;
		yield return con;

		if(con.error == null){
			pnl_erro.SetActive(false);
			pnl_todo.SetActive(true);
			btn_reconectar.interactable = true;			
		}else{
			btn_reconectar.interactable = true;
			print(con.error);
		}
	}

}
