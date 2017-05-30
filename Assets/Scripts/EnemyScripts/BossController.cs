using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public bool spearOn;
    public Sprite spriteLance;
    public Sprite spriteNoLance;
    public GameObject player;
    public float moveSpeed;
    Vector3 rotateTarget;
    public float minDist, coldowDoMelee, duracaoDaHitBoxDoMelee;
    public GameObject meleeHit;
    private bool onMeleeCd;
    private float countMelee;
    private GameObject projectile;
    public float shotDistance;
    public GameObject aim;
    public GameObject prefab;
    public GameObject watcher;

    // Use this for initialization
    void Start () {
        this.spearOn = false;
        this.GetComponent<SpriteRenderer>().color = Color.yellow;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        shotDistance = 8;
    }

    // Update is called once per frame
    void Update()
    {
        SetSprite();

        if (!player.GetComponent<CharacterActions>().spearOn && !spearOn) // player sem lança e boss sem lança, boss corre para ela
        {
            moveSpeed = this.GetComponent<EnemyHealth>().speed * 8 - this.GetComponent<EnemyHealth>().currentHealth;

            projectile = GameObject.FindGameObjectWithTag("Projectile");

            if (projectile.GetComponent<projectileScript>().speed == 0) // corre pra lança
            {

                float xPos = projectile.transform.position.x - this.transform.position.x;
                float yPos = projectile.transform.position.y - this.transform.position.y;
                float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
                float xPosNormalized = xPos / norm;
                float yPosNormalized = yPos / norm;

                Vector3 vetorDirecao = new Vector3(xPosNormalized, yPosNormalized, 0);
                rotateTarget.Set(projectile.transform.localPosition.x, projectile.transform.localPosition.y, -10.0f);
                Quaternion rot = Quaternion.LookRotation(transform.position - rotateTarget, Vector3.forward);
                transform.rotation = rot;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

                this.transform.Translate(vetorDirecao * moveSpeed * Time.deltaTime, Space.World);
            }
            else // corre pro player
            {
                moveSpeed = this.GetComponent<EnemyHealth>().speed * 2;

                rotateToPlayer();
            }
        }
        else if (player.GetComponent<CharacterActions>().spearOn) // player com a lança, boss corre pra ele
        {
            moveSpeed = this.GetComponent<EnemyHealth>().speed * 2;

            rotateToPlayer();
        }

        else if (spearOn) // boss com a lança
        {
            moveSpeed = this.GetComponent<EnemyHealth>().speed * 7 - this.GetComponent<EnemyHealth>().currentHealth;

            rotateToPlayerToShoot();
        }
    }



    public void rotateToPlayer()
    {
        float xPos = player.transform.position.x - this.transform.position.x;
        float yPos = player.transform.position.y - this.transform.position.y;
        float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
        float xPosNormalized = xPos / norm;
        float yPosNormalized = yPos / norm;

        Vector3 vetorDirecao = new Vector3(xPosNormalized, yPosNormalized, 0);
        rotateTarget.Set(player.transform.localPosition.x, player.transform.localPosition.y, -10.0f);
        Quaternion rot = Quaternion.LookRotation(transform.position - rotateTarget, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        this.transform.Translate(vetorDirecao * moveSpeed * Time.deltaTime, Space.World);
    }

    public void rotateToPlayerToShoot()
    {
        float xPos = player.transform.position.x - this.transform.position.x;
        float yPos = player.transform.position.y - this.transform.position.y;
        float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
        float xPosNormalized = xPos / norm;
        float yPosNormalized = yPos / norm;

        Vector3 vetorDirecao = new Vector3(xPosNormalized, yPosNormalized, 0);
        rotateTarget.Set(player.transform.localPosition.x, player.transform.localPosition.y, -10.0f);
        Quaternion rot = Quaternion.LookRotation(transform.position - rotateTarget, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        if ((Vector2.Distance(transform.position, player.transform.position) > shotDistance))
        {
            this.transform.Translate(vetorDirecao * moveSpeed * Time.deltaTime, Space.World);
        }

        else
        {
            this.moveSpeed = 0;
            throwLance(projectile, player);

        }
    }

    public void throwLance(GameObject projectile, GameObject target)
    {
        spearOn = false;
        GameObject spear = Instantiate(prefab, this.aim.transform.position, this.aim.transform.rotation);
        spear.gameObject.tag = "Enemy Projectile";
        spear.GetComponent<projectileScript>().speed = 0.8f;
        

    }

    public void attack()
    {
        if (!onMeleeCd)
        {
            meleeHit.GetComponent<SpriteRenderer>().enabled = true;
            meleeHit.GetComponent<PolygonCollider2D>().enabled = true;
            onMeleeCd = true;
        }
        

        if (onMeleeCd && countMelee > 0)
        {
            countMelee -= Time.deltaTime;
        }

        if (countMelee <= coldowDoMelee - duracaoDaHitBoxDoMelee)
        {
            meleeHit.GetComponent<PolygonCollider2D>().enabled = false;
            meleeHit.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (countMelee <= 0)
        {
            countMelee = coldowDoMelee;
            onMeleeCd = false;
        }

    }

    void SetSprite()
    {
        if (spearOn)
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteLance;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteNoLance;
        }

    }
}

