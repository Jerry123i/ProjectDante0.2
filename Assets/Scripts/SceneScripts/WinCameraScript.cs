using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCameraScript : MonoBehaviour {

    int revert;

	// Use this for initialization
	void Start () {

        revert = 1;

	}
	
	// Update is called once per frame
	void Update () {

       this.transform.Translate(0, Time.deltaTime / 4 * revert, 0);

        if(this.transform.localPosition.y >= 2.05f)
        {
            revert = -1;
        }

        if (this.transform.localPosition.y <= -2.05f)
        {
            revert = 1;
        }




    }
}
