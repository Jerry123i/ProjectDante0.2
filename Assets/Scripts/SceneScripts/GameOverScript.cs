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
	public GameObject prefabBoss;
	public GameObject prefabSpear;
	public bool bossStage = false;
	GameObject arena;
	GameObject spear;
	public bool freeze = false;

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
		goToBossStage ();

        if (playerHP <= 0)
        {
            EndCombat();
        }
		
	}

    void EndCombat()
    {

		if(hype < nextLevelHype || bossStage)
        {
            SceneManager.LoadScene("Game Over");
        }
		/*
        else if (hype < nextLevelHype)
        {
            SceneManager.LoadScene(stageNames[currentStage]);
        }
		*/

        else
        {

            SceneManager.LoadScene("BossCutscene");

            /*if (!bossStage) {
				GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag ("Enemy");
				for (int i = 0; i < enemiesToDestroy.Length; i++) {
					Destroy (enemiesToDestroy [i]);
				}
				GameObject[] enemyProjectilesToDestroy = GameObject.FindGameObjectsWithTag ("Enemy Projectile");
				for (int i = 0; i < enemyProjectilesToDestroy.Length; i++) {
					Destroy (enemyProjectilesToDestroy [i]);
				}
				GameObject[] projectilesToDestroy = GameObject.FindGameObjectsWithTag ("Projectile");
				for (int i = 0; i < projectilesToDestroy.Length; i++) {
					Destroy (projectilesToDestroy [i]);
				}
				GameObject[] sawsToDestroy = GameObject.FindGameObjectsWithTag ("Hazard");
				for (int i = 0; i < sawsToDestroy.Length; i++) {
					Destroy (sawsToDestroy [i]);
				}

				jogador.transform.position = new Vector3 (-9.88f, -0.045f, 0);
				jogador.GetComponent<PlayerHealth> ().currentHealth = jogador.GetComponent<PlayerHealth> ().totalHealth;
				jogador.GetComponent<PlayerHealth> ().sangue.fillAmount = 1;
				jogador.GetComponent<PlayerHealth> ().destroy = 1;
				playerHP = jogador.GetComponent<PlayerHealth> ().currentHealth;
				GameObject boss = Instantiate (prefabBoss, jogador.transform.position + new Vector3 ((-2) * jogador.transform.position.x, 0, 0), jogador.transform.rotation);
				jogador.GetComponent<CharacterActions> ().spearOn = false;
				arena = GameObject.FindGameObjectWithTag ("Wall");
				arena.GetComponent<SpawnerScript> ().enabled = false;
				spear = Instantiate (prefabSpear, (jogador.transform.position + boss.transform.position) / 2, transform.rotation);
				spear.GetComponent<projectileScript> ().speed = 0;
				spear.GetComponent<projectileScript> ().isProjectile = false;

				StartCoroutine (enterBossStage ());

				bossStage = true;
			}*/


        }


    }

    void UpdateHypeBar()
    {
        if (hype/nextLevelHype <= 1)
        {
            hypeBar.fillAmount = hype / nextLevelHype;
        }
    }

	IEnumerator enterBossStage() {
		freeze = true;
		arena.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.white;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.white;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.white;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.3f);
		arena.GetComponent<SpriteRenderer> ().color = Color.white;
		freeze = false;
	}

	void goToBossStage() { // aperatr B para ir direto para o boss stage
		if (Input.GetKey (KeyCode.B) && !bossStage) {
			hype = nextLevelHype + 1;
			playerHP = 0;
		}
	}
}
