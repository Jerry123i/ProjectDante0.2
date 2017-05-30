using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D enemy;

    public Transform player;
    private float minDist = 0.5f;

    Vector3 rotateTarget;
    
    float moveSpeed;

    // Use this for initialization
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

         moveSpeed = this.GetComponent<EnemyHealth>().speed;

         float xPos = player.position.x - enemy.position.x;
         float yPos = player.position.y - enemy.position.y;
         float norm = Mathf.Sqrt(xPos * xPos + yPos * yPos);
         float xPosNormalized = xPos / norm;
         float yPosNormalized = yPos / norm;

         Vector3 vetorDirecao = new Vector3(xPosNormalized, yPosNormalized, 0);
        
         if (Vector2.Distance(enemy.transform.position, player.position) > minDist)
         {
             enemy.transform.Translate(vetorDirecao * moveSpeed * Time.deltaTime, Space.World);
         }


        rotateTarget.Set(player.localPosition.x, player.localPosition.y, -10.0f);
        Quaternion rot = Quaternion.LookRotation(transform.position - rotateTarget, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);


    }

   
}
