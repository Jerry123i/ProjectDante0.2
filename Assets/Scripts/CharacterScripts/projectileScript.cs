using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour {

    public int danoDaLanca;
    public float speed;
    public Transform thisTransform;
    public bool isProjectile;
    public int nOfHits;
    GameObject watcher;
    public AudioClip hitSound;
    public AudioClip pickupSound;
    bool travar;
    
	void Start () {
        nOfHits = 0;
        isProjectile = true;
        watcher = GameObject.FindGameObjectWithTag("Watcher");
	travar = GameObject.Find("Controlador").GetComponent<pauseMenu>().paused;
	}
	
	void Update () {
        travar = GameObject.Find("Controlador").GetComponent<pauseMenu>().paused;
    }
	
	void LateUpdate () { //talvez por no lateupdate

        if (isProjectile && travar == false){
            thisTransform.Translate(Vector3.up * speed);
        }

		if (this.transform.position == new Vector3 (0, 0, 0) && this.transform.rotation.x == 0 && this.transform.rotation.y == 0 && this.transform.rotation.z == 0 && this.speed == 0) {
			isProjectile = false;
		}
	}


    private void OnTriggerEnter2D(Collider2D collisionTrig)
    {

        if (isProjectile)
        {
            if (collisionTrig.gameObject.tag == "Wall")
            {
				this.speed = 0;
				isProjectile = false;
				this.tag = "Projectile";

                if (nOfHits >= 2)
                {
                    GameObject.FindGameObjectWithTag("Crowd").GetComponent<CrowdScript>().Cheer(nOfHits - 1);
                }

            }

			if (collisionTrig.gameObject.tag == "Enemy" || collisionTrig.gameObject.tag == "Boss")
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

			if (collisionTrig.gameObject.tag == "Player") {
				if (this.tag == "Enemy Projectile") {
					collisionTrig.GetComponent<PlayerHealth> ().Damage ();
					if (collisionTrig.GetComponent<PlayerHealth> ().invunerable == false)
						StartCoroutine (collisionTrig.GetComponent<PlayerHealth> ().afterDamage ());
				}
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

			if (collisionTrig.gameObject.tag == "Boss") {
				collisionTrig.gameObject.GetComponent<BossController> ().spearOn = true;
				GameObject.FindGameObjectWithTag ("Watcher").GetComponent<AudioSource> ().PlayOneShot (pickupSound, 0.4f);
				Destroy (this.gameObject);
			}
        }
    }
}
