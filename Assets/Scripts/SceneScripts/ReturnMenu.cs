using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnMenu : MonoBehaviour {

	public Image black;
	public Animator anim;

	void Start () {
		
	}

    void ReturningToMenu () {
		if (Input.GetKey (KeyCode.Escape)) {
			StartCoroutine (Fading ());
			Application.LoadLevel ("Menu");
		}
    }
	

	public IEnumerator Fading() {
		anim.SetBool ("Fade", true);
		yield return new WaitUntil(() => black.color.a == 1);
	}

	void Update () {
        ReturningToMenu ();
	}
}
