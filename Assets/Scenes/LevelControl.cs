using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

	//public int index;
	//public string levelName0, levelName1, levelName2;

	public Image black;
	public Animator anim;

	/*void Condition (){
		if (Input.GetKey (KeyCode.Return) || Input.GetKey (KeyCode.Escape))
			StartCoroutine (Fading ());
	}*/

	public IEnumerator Fading() {
		anim.SetBool ("Fade", true);
		yield return new WaitUntil(() => black.color.a == 1);
		//SceneManager.LoadScene ("HighScore");
	}

	/*void Update(){
		Condition ();
	}*/
}