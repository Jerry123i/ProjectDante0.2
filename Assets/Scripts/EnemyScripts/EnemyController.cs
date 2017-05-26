using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D enemy;

    public Transform player;
    private float minDist = 0.5f;


    
    public float moveSpeed;

    // Use this for initialization
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float xPos = player.position.x - enemy.position.x;
        float yPos = player.position.y - enemy.position.y;
        float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
        float xPosNormalized = xPos / norm;
        float yPosNormalized = yPos / norm;

        Vector3 position = new Vector3(xPosNormalized, yPosNormalized, 0);

        //transform.up = (player.position - this.transform.position).normalized;

        if (Vector2.Distance(enemy.transform.position, player.position) > minDist)
        {
            enemy.transform.Translate(position * moveSpeed * Time.deltaTime);
        }

        

        
        
    }

   
}
