using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Classe_MascaraSenha : MonoBehaviour {

	public Sprite view, hide;
	bool senhaAMostra = false;

	public void Metodo_Mascara(){
		InputField txt_senha = GameObject.Find("txt_senha").GetComponent<InputField>();
		Image imagem_mascara = GameObject.Find("img_mascara").GetComponent<Image>();

		if(senhaAMostra){
			txt_senha.contentType = InputField.ContentType.Password;
			txt_senha.Select();

			imagem_mascara.sprite = view;
			senhaAMostra = false;
		}else{
			txt_senha.contentType = InputField.ContentType.Standard;
			txt_senha.Select();

			imagem_mascara.sprite = hide;
			senhaAMostra = true;			
		}
		
	}
}
