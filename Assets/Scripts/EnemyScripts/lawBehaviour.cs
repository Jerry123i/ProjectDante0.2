using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lawBehaviour : MonoBehaviour {

    public float limitex, limitey, limite_y, limite_x;
    public float speedx = 6, speedy = 6, counter = 0;
    bool forward = true, up = true, change = false;
    int trocou = 0, changer = 0;
    bool quebrar = false;
    public GameObject serrinha;

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

        if ((changer == 10 && counter < 10.02) || (changer == 20 && counter < 20.02) || (changer == 30 && counter < 30.02)) {
            speedx++;
            speedy++;
        }

        if ((changer == 15 && counter < 15.02) || (changer == 35 && counter < 35.02))
            change = true;

        if (change) {
            if (trocou == 0) {
                speedx = 4.5f;
                speedy += 2;
                trocou++;
            }
            else {
                speedy = speedx;
                speedx = 14.5f;
            }

            change = false;
        }


        counter += Time.deltaTime;
        changer = (int)counter;

        if(counter > 35) {
            this.GetComponent<Renderer>().material.color = Color.red;
            quebrar = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Player" && quebrar == true) {
            GameObject summon1 = Instantiate(serrinha) as GameObject;
            GameObject summon2 = Instantiate(serrinha) as GameObject;
            summon1.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            summon2.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			summon1.GetComponent<littleLawBehaviour>().up = !summon1.GetComponent<littleLawBehaviour>().up;
			summon2.GetComponent<littleLawBehaviour> ().forward = !summon2.GetComponent<littleLawBehaviour>().forward;
            Destroy(this.gameObject);
        }

    }


}
