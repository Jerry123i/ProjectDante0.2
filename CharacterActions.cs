using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour {

    public bool spearOn, onMeleeCd;
    float speed, teleport, turnSpeed, countMelee;
    public GameObject prefab;
    public GameObject aim; //Objeto a frente do player para evitar overlap do projetil com o player
    public GameObject meleeHit;
    private Camera mainCamera;
    bool invalidTeleport = false;
    private GameObject[] walls;

    void Start () {
        this.spearOn = true;
        this.onMeleeCd = false;
        this.countMelee = 0.2f;
        this.speed = 5f;
        this.teleport = 100f;
        this.turnSpeed = 10000000f;
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }
	
    void Moving() {
        if (Input.GetKey(KeyCode.A))
            this.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W))
            this.transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            this.transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            this.transform.position += ((new Vector3(-speed, speed, 0)).normalized) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            this.transform.position += ((new Vector3(speed, speed, 0)).normalized) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            this.transform.position += ((new Vector3(speed, -speed, 0)).normalized) * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            this.transform.position += ((new Vector3(-speed, -speed, 0)).normalized) * speed * Time.deltaTime;

        // This part of the code make the character look at the mouse position
        Vector2 lookToWard = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float targetAngle = Mathf.Atan2(lookToWard.y, lookToWard.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed*Time.deltaTime);

    }

    void Teleporting()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (-teleport), 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+teleport), 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(0, this.transform.position.y + (+teleport), 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(0, this.transform.position.y + (-teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (-teleport), this.transform.position.y + (+teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+teleport), this.transform.position.y + (+teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+teleport), this.transform.position.y + (-teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (-teleport), this.transform.position.y + (-teleport), 0) * Time.deltaTime;
    }

    void Shooting() {
        if (Input.GetMouseButtonDown(1) && spearOn) {
            GameObject projectile = Instantiate(prefab, aim.transform.position, aim.transform.rotation) as GameObject;
            spearOn = false;          
        }
    }

    void Melee()
    {
        if (Input.GetMouseButtonDown(0) && !onMeleeCd)
        {
            meleeHit.GetComponent<BoxCollider2D>().enabled = true;
            onMeleeCd = true;
        }

        if (onMeleeCd && countMelee > 0)
        {
            countMelee -= Time.deltaTime;
            Debug.Log(countMelee);
        }

        if (countMelee <= 0)
        {
            countMelee = 2.0f;
            onMeleeCd = false;
            meleeHit.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        for (int i = 0; i < walls.Length; i++)
        {
            if (collision.gameObject.tag == "Wall")
            {
                invalidTeleport = true;
            }
        }
        */
        if(collision.gameObject.tag == "Enemy")
        {
            transform.position += new Vector3(collision.contacts[0].normal.x, collision.contacts[0].normal.y, 0) * 1.5f;
        }
    }

    void Update () {
        Moving ();
        Melee();
        Teleporting();
        Shooting ();
	}

}
