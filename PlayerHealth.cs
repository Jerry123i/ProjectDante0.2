using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public Texture design, sangue;
    public int totalHealth = 4;
    public int currentHealth;
    int destroy = 1;
    AudioSource playerAudioSource;
    private bool invunerable = false;
    Color collideColor = Color.red;
    Color normalColor;

    void Start() {
        currentHealth = totalHealth;   
        playerAudioSource = GetComponent<AudioSource>();
        normalColor = this.GetComponent<Renderer>().material.color;
    }

    IEnumerator afterDamage()
    {
        invunerable = true;
        this.GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = normalColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = normalColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = normalColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = collideColor;
        yield return new WaitForSeconds(0.1f);
        this.GetComponent<Renderer>().material.color = normalColor;

        invunerable = false;
    }

    void Damage() {
        if (invunerable == false)
        {
            currentHealth--;

            switch (destroy)
            {
                case 1:
                    Destroy(GameObject.Find("hp1"));
                    break;
                case 2:
                    Destroy(GameObject.Find("hp2"));
                    break;
                case 3:
                    Destroy(GameObject.Find("hp3"));
                    break;
                default:
                    Destroy(GameObject.Find("hp4"));
                    break;
            }
            destroy++;
            playerAudioSource.Play();
        }
    }

    void Heal() {
        currentHealth++;
        if (currentHealth > totalHealth)
            currentHealth = totalHealth;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            Damage();
            if(invunerable == false) StartCoroutine(afterDamage());
        }
        if (coll.gameObject.tag == "Enemy Projectile")
        {
            Damage();
            if (invunerable == false) StartCoroutine(afterDamage());
        }
            
    }
}
  