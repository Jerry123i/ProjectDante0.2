using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {
    
    bool fadingOut;
    bool fadingIn;
    public Image black;

    void Start()
    {
        fadingIn = true;
    }

    void Update()
    {
        if(fadingIn)
        {
            black.color -= new Color(black.color.r, black.color.g, black.color.b, Time.deltaTime);
            if (black.color.a <= 0)
            {
                fadingIn = false;
            }
        }

        if (fadingOut)
        {
            black.color += new Color(black.color.r, black.color.g, black.color.b, Time.deltaTime);
        }

        if (black.color.a >= 1)
        {
            Application.LoadLevel("StageOne");
        }

        
    }

	public void Play (){

        fadingOut = true;       

	}

	public void Instrucoes(){
		Application.LoadLevel ("Instrucoes");
	}

	public void Creditos(){
		Application.LoadLevel ("Creditos");
	}

    public void Menu()
    {
        Debug.Log("Menu");
        Application.LoadLevel("Menu");
    }
}
