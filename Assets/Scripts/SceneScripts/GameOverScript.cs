using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    public int   playerHP;
    GameObject jogador;
    public float hype;        //Pontuacao final do jogador quando ele morrer
    public float minimalHype; //Pontuacao que o jogador deve ter para não perder o jogo
    public float nextLevelHype; //Pontuacao que o jogador deve ter para passar de nível

    public string[] stageNames; //nome dos arquivos de cena de cada fase
    public int currentStage;

    public Image hypeBar;

	// Use this for initialization
	void Start () {

        jogador = GameObject.FindWithTag("Player");
        hypeBar.fillAmount = 0;
      //  hypeBar.maxValue = nextLevelHype;

	}
	
	// Update is called once per frame
	void Update () {

        UpdateHypeBar();

        playerHP = jogador.GetComponent<PlayerHealth>().currentHealth;

        if (playerHP <= 0)
        {
            EndCombat();
        }
		
	}

    void EndCombat()
    {

        if(hype <= minimalHype)
        {
            SceneManager.LoadScene("GameOver");
        }

        else if (hype < nextLevelHype)
        {
            SceneManager.LoadScene(stageNames[currentStage]);
        }

        else if (hype >= nextLevelHype)
        {
            currentStage++;
            SceneManager.LoadScene(stageNames[currentStage]);
        }


    }

    void UpdateHypeBar()
    {
        if (hype/nextLevelHype <= 1)
        {
            hypeBar.fillAmount = hype / nextLevelHype;
        }
    }
}
