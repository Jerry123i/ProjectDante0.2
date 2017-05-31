using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int totalHealth = 4;
    public int currentHealth;
    public int destroy = 1;
    public Image sangue;
    AudioSource playerAudioSource;
    bool travar;
    public bool invunerable = false;
    Color collideColor = Color.red;
    Color normalColor;

    float debugClock;

    void Start() {
        debugClock = 0;
        currentHealth = totalHealth;
        playerAudioSource = GetComponent<AudioSource>();
        travar = GameObject.Find("Controlador").GetComponent<pauseMenu>().paused;
        normalColor = this.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        if (invunerable)
        {
            debugClock += Time.deltaTime;

            if (debugClock >= 1f)
            {
                invunerable = false;
                this.GetComponent<Renderer>().material.color = normalColor;
                debugClock = 0;
            }
        }
        else
        {
            debugClock = 0;
        }
    }

    public IEnumerator afterDamage()
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

    public void Damage() {
        if (invunerable == false)
        {
            currentHealth--;
            
            switch(destroy) {
                case 1:
                    sangue.fillAmount = 0.8081f;
                    break;
                case 2:
                    sangue.fillAmount = 0.499f;
                    break;
                case 3:
                    sangue.fillAmount = 0.188f;
                    break;
                default:
                    sangue.fillAmount = 0;
                    break;
        
            }
            destroy++;
            playerAudioSource.Play();
        }
        
		/*
        if (currentHealth <= 0) {
            Application.LoadLevel("Game Over");
        }
        */
    }

    void Heal() {
        currentHealth++;
        if (currentHealth > totalHealth)
            currentHealth = totalHealth;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Hazard" || coll.gameObject.tag == "Boss") {
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
  