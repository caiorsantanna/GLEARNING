using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Movimento_Horizontal_Atividades : MonoBehaviour {
	RectTransform rectTransform;
	Touch touch;
	float vel = 0.5f;
	float top, left, right, bottom, pX, pY;
	void Start(){
		rectTransform = GetComponent<RectTransform>();
	}
	void Update(){
		if(Input.touchCount > 0){
			touch = Input.GetTouch(0);

			top 	= (rectTransform.position.y + (rectTransform.rect.height/2));
			bottom 	= (rectTransform.position.y - (rectTransform.rect.height/2));
			left	= (rectTransform.position.x - (rectTransform.rect.width/2));
			right	= (rectTransform.position.x + (rectTransform.rect.width/2));

			pX = touch.position.x;
			pY = touch.position.y;

			if((pX > left)&&(pX < right)&&(pY > bottom)&&(pY < top)){
				rectTransform.Translate(touch.deltaPosition.x, 0, 0);
			}			
			print("("+pX+"/"+pY+") - "+top+" "+bottom+" "+left+" "+right);
		}
	}
}
