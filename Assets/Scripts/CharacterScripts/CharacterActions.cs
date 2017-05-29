using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour {

    public float coldowDoMelee;
    public float duracaoDaHitBoxDoMelee;

    public Image CdImage;

    public bool spearOn, onMeleeCd;
    float speed, teleport, turnSpeed, countMelee;
    public Sprite spriteSemLanca;
    public Sprite spriteLanca;
    public GameObject prefab;
    public GameObject aim; //Objeto a frente do player para evitar overlap do projetil com o player
    public GameObject meleeHit;
    private Camera mainCamera;
    Vector3 mousePosition;
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

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);


    }

    void Teleporting () {
        if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport) , 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(0, this.transform.position.y + (+ teleport), 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(0, this.transform.position.y + (- teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport), this.transform.position.y + ( + teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), this.transform.position.y + (+ teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), this.transform.position.y + (- teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport), this.transform.position.y + (- teleport), 0) * Time.deltaTime;
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
            meleeHit.GetComponent<PolygonCollider2D>().enabled = true;
            meleeHit.GetComponent<SpriteRenderer>().enabled = true;
            onMeleeCd = true;
        }

        if (onMeleeCd && countMelee > 0)
        {
            countMelee -= Time.deltaTime;
            CdImage.fillAmount = (coldowDoMelee - countMelee) / coldowDoMelee;
        }

        if (countMelee <= coldowDoMelee-duracaoDaHitBoxDoMelee)
        {            
            meleeHit.GetComponent<PolygonCollider2D>().enabled = false;
            meleeHit.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (countMelee <= 0)
        {
            countMelee = coldowDoMelee;
            CdImage.fillAmount = 0f;
            onMeleeCd = false;
        } 

    }

    void SetSprite()
    {
        if (spearOn)
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteLanca;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteSemLanca;
        }

    }

	void Update () {
        Moving ();
        Melee();
        Teleporting ();
        Shooting ();
        SetSprite();
	}
}
