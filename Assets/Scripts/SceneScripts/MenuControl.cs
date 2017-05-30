using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {


	public void Play (){
		Application.LoadLevel("StageOne");
	}

	public void Instrucoes(){
		Application.LoadLevel ("Instrucoes");
	}

	public void Creditos(){
		Application.LoadLevel ("Creditos");
	}
}
