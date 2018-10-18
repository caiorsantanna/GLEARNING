using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classe_Movimentação : MonoBehaviour {

	SimpleInputNamespace.Joystick joystick;
	Transform tPlayer;
	Transform tCamera;
	
	void Start(){
		joystick = GameObject.Find("Joystick").GetComponent<SimpleInputNamespace.Joystick>();
		tCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
		tPlayer = GetComponent<Transform>();	
	}
	void Update () {
		tPlayer.position = new Vector3((tPlayer.position.x + joystick.xAxis.value/5),(tPlayer.position.y + joystick.yAxis.value/5), 0f);
		tCamera.position = new Vector3(tPlayer.position.x, tPlayer.position.y, -10);
	}
}
