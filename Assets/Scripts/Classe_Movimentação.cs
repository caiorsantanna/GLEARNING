using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Movimentação : MonoBehaviour {

	SimpleInputNamespace.Joystick joystick;
	Transform tCamera;
	Rigidbody2D rPlayer;
	Animator aPlayer;

	public bool cima = false, baixo = false, direita = false, esquerda = false;
	
	void Start(){
		joystick = GameObject.Find("Joystick").GetComponent<SimpleInputNamespace.Joystick>();
		tCamera = GameObject.Find("Main Camera").GetComponent<Transform>();				
		rPlayer = GetComponent<Rigidbody2D>();
		aPlayer = GetComponent<Animator>();
	}
	void FixedUpdate () {
		float xAxis = joystick.xAxis.value;
		float yAxis = joystick.yAxis.value;
		float absXAxis = System.Math.Abs(xAxis);
		float absYAxis = System.Math.Abs(yAxis);

		rPlayer.velocity = new Vector2(xAxis * 5, yAxis * 5);		
		tCamera.position = new Vector3(rPlayer.position.x, rPlayer.position.y, -10);

		if((xAxis > 0)&&(yAxis > 0)){
			if(yAxis > xAxis){
				cima = true;
				baixo = false;
				direita = false;
				esquerda = false;
			}else{
				cima = false;
				baixo = false;
				direita = true;
				esquerda = false;
			}			
		}else if((xAxis > 0)&&(yAxis < 0)){
			if(absYAxis > xAxis){
				cima = false;
				baixo = true;
				direita = false;
				esquerda = false;
			}else{
				cima = false;
				baixo = false;
				direita = true;
				esquerda = false;
			}
		}else if((xAxis < 0)&&(yAxis > 0)){
			if(yAxis > absXAxis){
				cima = true;
				baixo = false;
				direita = false;
				esquerda = false;
			}else{
				cima = false;
				baixo = false;
				direita = false;
				esquerda = true;
			}
		}else if((xAxis < 0)&&(yAxis < 0)){
			if(absYAxis > absXAxis){
				cima = false;
				baixo = true;
				direita = false;
				esquerda = false;
			}else{
				cima = false;
				baixo = false;
				direita = false;
				esquerda = true;
			}
		}else{
			cima = false;
			baixo = false;
			direita = false;
			esquerda = false;
		}

		aPlayer.SetBool("direita",direita);
		aPlayer.SetBool("esquerda",esquerda);
		aPlayer.SetBool("cima",cima);
		aPlayer.SetBool("baixo",baixo);
	}
}
