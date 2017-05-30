using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleLawBehaviour : MonoBehaviour {

    public float limitex, limitey, limite_y, limite_x;
    public float speedx = 12, speedy = 12, counter = 0;
    public bool forward = true, up = true, change = false;
    

    void Start() {

    }


    void Update() {
        GetComponent<Rigidbody2D>().WakeUp();

        if (forward)
            transform.Translate(speedx * Time.deltaTime, 0, 0);
        else
            transform.Translate(-speedx * Time.deltaTime, 0, 0);

        if (up)
            transform.Translate(0, speedy * Time.deltaTime, 0);
        else
            transform.Translate(0, -speedy * Time.deltaTime, 0);

        if (transform.position.x > limitex)
            forward = false;
        if (transform.position.x < limite_x)
            forward = true;

        if (transform.position.y > limitey)
            up = false;
        if (transform.position.y < limite_y)
            up = true;
    }

    


}
