using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public AudioClip deathSound;

    public int totalHealth; 
    public int currentHealth;

    public Sprite spriteNormal;
    public Sprite spriteInjured;

    public bool onMeleeHit;
    public float hitCD;
    public float meleeClock;

    void Start() {
        currentHealth = totalHealth; // Começa dando a vida específica total do monstro para a vida atual
        meleeClock = hitCD;
        onMeleeHit = false;
                   
    }

    void Update()
    {
        if (currentHealth == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteInjured;
        }       

        if (onMeleeHit)
        {
            RunClock();
        }
    
    }

    public void Damage(int dmg) { //Função chamada quando a hitbox for atingida
        currentHealth -=dmg; // Supondo que o personagem bata 1. Facilmente alterável.

        if (currentHealth <= 0)  // Se a vida chega em 0, deleta ele da cena
        {
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().pitch += Random.Range(-0.3f, 0.3f);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().PlayOneShot(deathSound, 0.4f);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().pitch = 1.0f;
            Destroy(this.gameObject);
        }
            
    }

    void RunClock()
    {
        meleeClock -= Time.deltaTime;

        if (meleeClock <= 0)
        {
            onMeleeHit = false;
            meleeClock = hitCD;
        }
    }


}
