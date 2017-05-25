using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public AudioClip deathSound;

    public int totalHealth; 
    public int currentHealth;

    void Start() {
        currentHealth = totalHealth; // Começa dando a vida específica total do monstro para a vida atual
       
    }

    public void Damage(int dmg) { //Função chamada quando a hitbox for atingida
        currentHealth -=dmg; // Supondo que o personagem bata 1. Facilmente alterável.

        if (currentHealth <= 0)  // Se a vida chega em 0, deleta ele da cena
        {
            Debug.Log(currentHealth);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().pitch += Random.Range(-0.3f, 0.3f);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().PlayOneShot(deathSound, 0.4f);
            GameObject.FindGameObjectWithTag("Watcher").GetComponent<AudioSource>().pitch = 1.0f;
            Destroy(gameObject);
        }
            
    }


}
