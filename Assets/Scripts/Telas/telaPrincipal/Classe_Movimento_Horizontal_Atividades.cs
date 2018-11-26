using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Movimento_Horizontal_Atividades : MonoBehaviour {
	RectTransform rectTransform;	
	Touch touch;	
	float top, left, right, bottom, pX, pY, finalLeft, finalRight;
	void Start(){
		rectTransform = GetComponent<RectTransform>();
		finalLeft = (rectTransform.position.x - rectTransform.rect.width);
		finalRight = (rectTransform.position.x + rectTransform.rect.width/2);		
	}
	void FixedUpdate(){
		if(Input.touchCount > 0){
			touch = Input.GetTouch(0);
			
			top 	= (rectTransform.position.y + (rectTransform.rect.height/2));
			bottom 	= (rectTransform.position.y - (rectTransform.rect.height/2));
			left	= (rectTransform.position.x - (rectTransform.rect.width/2));
			right	= (rectTransform.position.x + (rectTransform.rect.width/2));

			pX = touch.position.x;
			pY = touch.position.y;
			
			if((left >= finalLeft)&&(right <= finalRight)){
				if((pX > left)&&(pX < right)&&(pY > bottom)&&(pY < top)){				
					rectTransform.anchoredPosition += new Vector2(touch.deltaPosition.x, 0);					
				}	
			}

			
			if(left < finalLeft){								
				rectTransform.position = new Vector2(finalLeft+ (rectTransform.rect.width/2), rectTransform.position.y);
			}
			if(right > finalRight){
				rectTransform.position = new Vector2(finalRight - (rectTransform.rect.width/2), rectTransform.position.y);
			}
			//print("("+pX+"/"+pY+") - T:"+top+" B:"+bottom+" L:"+left+" R:"+right+" FL:"+finalLeft+" FR:"+finalRight);


			// if(touch.phase == TouchPhase.Moved){
			// 	if(rectTransform.rect.Contains(touch.position)){
			// 		rectTransform.Translate(touch.deltaPosition.x, 0, 0);
			// 	}				
			// }			
			
		}
	}
}
