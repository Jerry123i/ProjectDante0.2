using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    public int danoDaLanca;
    public float speed;
    public Transform thisTransform;
    bool isProjectile;
    public int nOfHits;
    GameObject watcher;
    public AudioClip hitSound;
    public AudioClip pickupSound;
    
	void Start () {
        nOfHits = 0;
        isProjectile = true;
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
                if (collisionTrig.gameObject.GetComponent<EnemyHealth>().currentHealth <= danoDaLanca)
                {
                    nOfHits++;
                }

                if (nOfHits == 1)
                {
                    watcher.GetComponent<GameOverScript>().hype += 1;
                }
                else
                {
                    watcher.GetComponent<GameOverScript>().hype += Mathf.Pow(3, (nOfHits-1));
                }

                collisionTrig.gameObject.GetComponent<EnemyHealth>().Damage(danoDaLanca);
                this.GetComponent<AudioSource>().pitch = 1 + ((nOfHits+1) * 0.5f);
                this.GetComponent<AudioSource>().PlayOneShot(hitSound, 1.0f);

            }

        }


        else
        {            
            if (collisionTrig.gameObject.tag == "Player")
            {
                collisionTrig.gameObject.GetComponent<CharacterActions>().spearOn = true;
                GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().PlayOneShot(pickupSound, 0.4f);
                Destroy(this.gameObject);               
            }
        }
    }
}
