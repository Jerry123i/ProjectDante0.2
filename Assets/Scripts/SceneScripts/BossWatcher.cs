using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWatcher : MonoBehaviour {

    public GameObject theBoss;
    public GameObject thePlayer;
    

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        
        if (theBoss.gameObject == null)
        {
            SceneManager.LoadScene("WinScene");
        }
		
	}
}
