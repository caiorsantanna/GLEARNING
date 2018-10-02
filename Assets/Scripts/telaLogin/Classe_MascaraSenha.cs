using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_MascaraSenha : MonoBehaviour {

	public Text senha;
	public Font fonteSenha;
	public Font fonteMascara;
	public Image imagemMascara;
	public Sprite view, hide;
	bool senhaAMostra = false;

	public void esconder_mostrarSenha(){
		if(senhaAMostra){
			senha.font = fonteMascara;
			//senha.resizeTextForBestFit = false;
			imagemMascara.sprite = view;
			senhaAMostra = false;
			//print("macara false");
		}else{
			senha.font = fonteSenha;
			//senha.resizeTextForBestFit = true;
			imagemMascara.sprite = hide;
			senhaAMostra = true;
			//print("mascara true");
		}
	}
}
