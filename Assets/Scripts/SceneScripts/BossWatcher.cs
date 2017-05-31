using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossWatcher : MonoBehaviour {

    public GameObject theBoss;
    public GameObject thePlayer;
    public GameObject theProjectile;


    private void Awake()
    {
        theProjectile.GetComponent<projectileScript>().speed = 0;
        
        thePlayer.GetComponent<CharacterActions>().spearOn = false;
    }

    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {

        thePlayer.GetComponent<CharacterActions>().SetSprite();
        if (theProjectile.transform.position == new Vector3(0, 0, 0) && theProjectile.GetComponent<projectileScript>().speed == 0 && theProjectile.transform.rotation.x == 0 && theProjectile.transform.rotation.y == 0 && theProjectile.transform.rotation.z == 0)
        {
            theProjectile.GetComponent<projectileScript>().isProjectile = false;
        }

        if (theBoss.gameObject == null)
        {
            SceneManager.LoadScene("WinScene");
        }
		
	}
}
