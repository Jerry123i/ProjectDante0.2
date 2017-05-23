using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorGameOver: MonoBehaviour {

	public void menuPrincipal() {
        Application.LoadLevel("Menu");
    }

    public void jogarNovamente() {
        Application.LoadLevel("StageOne");
    }
	
    public void highScores() {
        Application.LoadLevel("High Scores");
    }
	
}
