using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdScript : MonoBehaviour {

    public AudioClip[] comemoracoes;
    public AudioClip clipAtual;
    public float t;
    public int cheerConstant;
    public bool comemorando;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

        if (comemorando)
        {
            t += Time.deltaTime;
            this.GetComponent<AudioSource>().volume = ((-cheerConstant * (t * t)) + cheerConstant*2)/6;

            if (t >= 1.5f)
            {
                t = 0;
                comemorando = false;
            } 
        }

	}

    public void Cheer(int combo)
    {
        comemorando = true;
        cheerConstant = combo;
        clipAtual = comemoracoes[Random.Range(0, comemoracoes.Length)];
        t = -1.5f;
        this.GetComponent<AudioSource>().clip = clipAtual;
        this.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 0.95f);
        this.GetComponent<AudioSource>().Play();

    }
}
