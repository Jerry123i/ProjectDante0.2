using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    private Rigidbody2D enemyRigidBody2D;
    public Transform playerTransform;
    public float shotDistance;
    public float rangedMoveSpeed;
    public GameObject enemyAim;
    public GameObject prefab;
    private bool canShoot = true;
    Transform enemyProjectileTransform;

    // Use this for initialization
    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
        enemyAim.transform.position = enemyRigidBody2D.transform.position + new Vector3(0.5f, 0.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rangedMoveSpeed = this.GetComponent<EnemyHealth>().speed;

        float xPos = playerTransform.position.x - enemyRigidBody2D.transform.position.x;
        float yPos = playerTransform.position.y - enemyRigidBody2D.transform.position.y;
        float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
        float xPosNormalized = xPos / norm;
        float yPosNormalized = yPos / norm;

        Vector3 position = new Vector3(xPosNormalized, yPosNormalized, 0);

        if (Vector2.Distance(enemyRigidBody2D.transform.position, playerTransform.position) > shotDistance)
        {
            enemyRigidBody2D.transform.Translate(position * rangedMoveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(enemyRigidBody2D.transform.position, playerTransform.position) == shotDistance)
        {
            shootPlayer();
        }
        else
        {
            enemyRigidBody2D.transform.Translate(position * -rangedMoveSpeed * Time.deltaTime);
            shootPlayer();
        }

        enemyAim.transform.LookAt(playerTransform);
        enemyAim.transform.up = playerTransform.position - enemyAim.transform.position;
    }

    IEnumerator afterShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }

    void shootPlayer()
    {
        if (canShoot)
        {
            GameObject enemyProjectile = Instantiate(prefab, enemyAim.transform.position, enemyAim.transform.rotation);
            enemyProjectile.GetComponent<projectileScript>().speed = 0.2f;
            enemyProjectileTransform = enemyProjectile.GetComponent<Transform>();
            enemyProjectileTransform.rotation = Random.rotation;
            enemyProjectile.GetComponent<Transform>().up = playerTransform.position - enemyProjectileTransform.position;
            StartCoroutine(afterShoot());
        }
    }
}
