using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour {

    public int danoDoMelee;

    public AudioClip audioMelee;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<EnemyHealth>().onMeleeHit)
        {
            collision.gameObject.GetComponent<EnemyHealth>().Damage(danoDoMelee);            
            collision.gameObject.GetComponent<EnemyHealth>().onMeleeHit = true;
            
            collision.gameObject.GetComponent<Rigidbody2D>().WakeUp();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.localPosition - GameObject.FindGameObjectWithTag("Player").transform.localPosition ).normalized * 8, ForceMode2D.Impulse);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().PlayOneShot(audioMelee, 0.3f);

        }
    }

}
