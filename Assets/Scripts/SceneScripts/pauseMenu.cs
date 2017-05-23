using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour {

    public bool paused, muted;
    public GameObject pause;

    void Start () {
        muted = false;
        paused = false;
        pause.SetActive(paused);
	}
	
	
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            pause.SetActive(paused);
        }

        if(paused) {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

	}


    public void unpause() {
        paused = !paused;
        pause.SetActive(paused);
    }

    public void menuPrincipal() {
        Application.LoadLevel("MenuPrincipal");
    }

    public void muteSound() {
        muted = !muted;
        AudioListener.pause = muted;

    }



}
