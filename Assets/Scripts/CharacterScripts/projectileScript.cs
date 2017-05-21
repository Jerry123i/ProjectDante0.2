using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    public float speed;
    public Transform thisTransform;
    BoxCollider2D collider;
    bool isProjectile;
    public int nOfHits;
    GameObject watcher;
    
	void Start () {
        nOfHits = 0;
        isProjectile = true;
        collider = GetComponent<BoxCollider2D>();
        watcher = GameObject.FindGameObjectWithTag("Watcher");
	}
	
	void LateUpdate () { //talvez por no lateupdate

        if (isProjectile){
            thisTransform.Translate(Vector3.up * speed);
        }
	}

   /* private void OnCollisionEnter2D(Collision2D coll)
    {        
        if (coll.collider.tag == "Wall")        {
            isProjectile = false;
            collider.isTrigger = true;            
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collisionTrig)
    {

        if (isProjectile)
        {
            if (collisionTrig.gameObject.tag == "Wall")
            {
                isProjectile = false;
            }

            if (collisionTrig.gameObject.tag == "Enemy")
            {
                nOfHits++;

                if (nOfHits == 1)
                {
                    watcher.GetComponent<GameOverScript>().hype += 1;
                }
                else
                {
                    watcher.GetComponent<GameOverScript>().hype += Mathf.Pow(3, (nOfHits-1));
                }
                Debug.Log(watcher.GetComponent<GameOverScript>().hype);
                Destroy(collisionTrig.gameObject);


            }

        }


        else
        {            
            if (collisionTrig.gameObject.tag == "Player")
            {
                collisionTrig.gameObject.GetComponent<CharacterActions>().spearOn = true;
                Destroy(this.gameObject);               
            }
        }
    }
}
