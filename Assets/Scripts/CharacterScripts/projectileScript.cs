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
    public AudioClip hitSound;
    
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


    private void OnTriggerEnter2D(Collider2D collisionTrig)
    {

        if (isProjectile)
        {
            if (collisionTrig.gameObject.tag == "Wall")
            {
                isProjectile = false;

                if (nOfHits >= 2)
                {
                    GameObject.FindGameObjectWithTag("Crowd").GetComponent<CrowdScript>().Cheer(nOfHits - 1);
                }

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

                collisionTrig.gameObject.GetComponent<EnemyHealth>().Damage(1);
                this.GetComponent<AudioSource>().pitch = 1 + ((nOfHits+1) * 0.5f);
                this.GetComponent<AudioSource>().PlayOneShot(hitSound, 1.0f);

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
