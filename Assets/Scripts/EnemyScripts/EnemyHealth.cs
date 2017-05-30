using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public AudioClip deathSound;
    
    public int totalHealth; 
    public int currentHealth;

    public float initialSpeed;
    public float speed;

    public Sprite spriteNormal;
    public Sprite spriteInjured;

    public bool onMeleeHit;
    public bool meleeEvents;
    public float hitCD;

    float meleeClock;
    bool skipOneFrame = false;

    public float slipTime;

    public float debugClock;
    public bool balancing;

    void Start() {

        currentHealth = totalHealth; // Começa dando a vida específica total do monstro para a vida atual
        speed = initialSpeed;
        meleeClock = 0;
        debugClock = 0;
        onMeleeHit = false;
                   
    }

    void Update()
    {
        //Verificador periódico da situação de movimento físico
        debugClock += Time.deltaTime;
        if (debugClock >= 3)
        {
            if (!meleeEvents){
                balancing = true;
            }
            debugClock = 0;
        }



		if (currentHealth == 1 && this.gameObject.tag == "Enemy")
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteInjured;
        }       

        if (meleeEvents)
        {
            RunClock();
        }

        if (balancing)
        {
            RandomPhysicRestaurer();
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
        meleeClock += Time.deltaTime;               

        if (meleeClock >= hitCD) //Restaura estado normal quando tempo zera
        {
            onMeleeHit = false;
        }

        if (skipOneFrame)
        {
            this.GetComponent<Rigidbody2D>().drag = 1f;
            this.GetComponent<Rigidbody2D>().mass = 1000000f;            
        }

        if (meleeClock >= slipTime) //Momento de parada
        {
           // RestaurarFisica();
            this.GetComponent<Rigidbody2D>().drag = 1000000f;
            
            skipOneFrame = true;

            speed = (meleeClock - slipTime) * (initialSpeed/(slipTime/2)); //Restauracao de velocidade

            if (speed>= initialSpeed) 
            {
                speed = initialSpeed;
                this.GetComponent<Rigidbody2D>().drag = 0;
                //balancing = true;
            }

        }

        if (meleeClock > hitCD && speed == initialSpeed)
        {
            meleeClock = 0;
            meleeEvents = false;
            skipOneFrame = false;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy" && coll.gameObject.GetComponent<EnemyHealth>().meleeEvents)
        {
            if (!meleeEvents)
            {
                meleeEvents = true;
            }
            else
            {
                
            }
            speed = 0;
            GetComponent<Rigidbody2D>().mass = 1f;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce((this.transform.localPosition - coll.transform.localPosition).normalized * 4, ForceMode2D.Impulse);
        }
    }    

    public void RandomPhysicRestaurer()
    {
        if (balancing)
        {
            if (this.GetComponent<Rigidbody2D>().drag == 1000000f && this.GetComponent<Rigidbody2D>().mass == 1f)
            {
                this.GetComponent<Rigidbody2D>().mass = 1000000f;
                this.GetComponent<Rigidbody2D>().drag = 0f;
                this.speed = initialSpeed;
                balancing = false;
            }

            if (balancing)
            {
                this.speed = 0;
                this.GetComponent<Rigidbody2D>().mass = 1f;
                this.GetComponent<Rigidbody2D>().drag = 1000000f;
            }
            
        }
    }

  


}
