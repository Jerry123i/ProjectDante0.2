using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterActions : MonoBehaviour {

    public float coldowDoMelee;
    public float duracaoDaHitBoxDoMelee;
    
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
    
    public float limitex, limite_x, limitey, limite_y;
    public float teleportCd;
    public bool isOnTeleportCooldown = false;
    public float teleportTimer = 0;
    Sprite original;
    public Sprite teleporte;
    public AudioSource teleporteSound;
    bool travar;

	public Image cdMeleeImage;
    public Image cdTeleporterImage;

	void Start () {
        this.onMeleeCd = false;
        this.countMelee = 0.2f;
        this.speed = 5f;
        this.teleport = 100f;
        this.turnSpeed = 10000000f;
        walls = GameObject.FindGameObjectsWithTag("Wall");
        original = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        travar = GameObject.Find("Controlador").GetComponent<pauseMenu>().paused;
    }
	
    void Moving() {
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime * 1.2f;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime * 1.2f;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, speed, 0) * Time.deltaTime * 1.2f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, -speed, 0) * Time.deltaTime * 1.2f;
        }


        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            this.transform.position += ((new Vector3(-speed + 10, speed + 10, 0)).normalized) * speed * Time.deltaTime / 1.2f;
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            this.transform.position += ((new Vector3(speed - 10, speed + 10, 0)).normalized) * speed * Time.deltaTime / 1.2f;
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            this.transform.position += ((new Vector3(speed - 10, -speed - 10, 0)).normalized) * speed * Time.deltaTime / 1.2f;
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            this.transform.position += ((new Vector3(-speed + 10, -speed - 10, 0)).normalized) * speed * Time.deltaTime / 1.2f;


        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.Set(mousePosition.x, mousePosition.y, -100000f);
        Quaternion rot = Quaternion.LookRotation(transform.localPosition - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);


    }

    void Teleporting () {

        if (Input.GetKey(KeyCode.A))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport) , 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), 0, 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W))
            this.transform.position += new Vector3(0, this.transform.position.y + (+ teleport), 0) * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            this.transform.position += new Vector3(0, this.transform.position.y + (- teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport), this.transform.position.y + ( + teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), this.transform.position.y + (+ teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            this.transform.position += new Vector3(this.transform.position.x + (+ teleport), this.transform.position.y + (- teleport), 0) * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            this.transform.position += new Vector3(this.transform.position.x + (- teleport), this.transform.position.y + (- teleport), 0) * Time.deltaTime;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = teleporte;
        teleporteSound.Play();
        isOnTeleportCooldown = true;
        teleportTimer = 0;
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
			cdMeleeImage.fillAmount = (coldowDoMelee - countMelee) / coldowDoMelee;
		}

		if (countMelee <= coldowDoMelee-duracaoDaHitBoxDoMelee)
		{            
			meleeHit.GetComponent<PolygonCollider2D>().enabled = false;
			meleeHit.GetComponent<SpriteRenderer>().enabled = false;
		}

		if (countMelee <= 0)
		{
			countMelee = coldowDoMelee;
			cdMeleeImage.fillAmount = 0f;
			onMeleeCd = false;
		} 


    }

    public void SetSprite()
    {
        if (spearOn)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteLanca;
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteSemLanca;
        }

    }

    void Update() {

        SetSprite();

        travar = GameObject.Find("Controlador").GetComponent<pauseMenu>().paused;
        if (travar == false) {
            Moving();
            Melee();
            Shooting();

            if (Input.GetKeyDown(KeyCode.Space) && isOnTeleportCooldown == false)
                Teleporting();

            teleportTimer += Time.deltaTime;

            if (isOnTeleportCooldown)
            {
                cdTeleporterImage.fillAmount = (teleportCd - teleportTimer) / teleportCd;
            }

            else
            {
                cdTeleporterImage.fillAmount = 0f;
            }            

            if (teleportTimer >= 1) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = original;
            }

            if (teleportTimer >= teleportCd)
                isOnTeleportCooldown = false;


            if (this.transform.position.x > limitex)
                this.transform.position -= new Vector3(this.transform.position.x - limitex, 0, 0);
            if (this.transform.position.x < limite_x)
                this.transform.position -= new Vector3(this.transform.position.x - limite_x, 0, 0);
            if (this.transform.position.y > limitey)
                this.transform.position -= new Vector3(0, this.transform.position.y - limitey, 0);
            if (this.transform.position.y < limite_y)
                this.transform.position -= new Vector3(0, this.transform.position.y - limite_y, 0);
        }
    }
}
