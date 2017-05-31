using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject[] spawners;
    GameObject HotSpawner;
    public GameObject[] enemies;
    public int[] spawnList;

    public Sprite pitCold;
    public Sprite pitHot;

    GameObject instance;

    bool activeSpawning;
    int nToSpawn;
    float spawnCDClock;
    public GameOverScript hypeHolder;

    int waves;
    float totalTime;


    // Use this for initialization
    void Start()
    {
        waves = 0;
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;

        if ((GameObject.FindGameObjectsWithTag("Enemy").Length == 0) && !activeSpawning)
        {
            activeSpawning = true;
            waves++;
            nToSpawn = EnemiesPerWave(waves);
            spawnList = WhatToSpawn(waves);
            HotSpawner = spawners[Random.Range(0, spawners.Length)];
            HotSpawner.GetComponent<SpriteRenderer>().sprite = pitHot;

        }

        if (activeSpawning)
        {
            if (nToSpawn > 0)
            {
                if (spawnCDClock>= 1.0f)
                {
                    instance = Instantiate(enemies[spawnList[nToSpawn-1]], HotSpawner.transform.position, Quaternion.Euler(Vector3.zero));
					
                    if(instance.GetComponent<EnemyHealth>() != null)
                    {
                        instance.GetComponent<EnemyHealth>().initialSpeed *= MultiplierPerTime(totalTime);
                        instance.GetComponent<EnemyHealth>().initialSpeed += Random.Range(-0.5f, 0.5f);
                        instance.GetComponent<EnemyHealth>().slipTime += Random.Range(-0.1f, 0.6f);
                    }

                    spawnCDClock = 0f;
                    nToSpawn--;

                    HotSpawner.GetComponent<SpriteRenderer>().sprite = pitCold;

                    if (nToSpawn > 0)
                    {
                        HotSpawner = spawners[Random.Range(0, spawners.Length)];
                        HotSpawner.GetComponent<SpriteRenderer>().sprite = pitHot;
                    }

                }

                else
                {
                    spawnCDClock += Time.deltaTime;
                }

            }

            else
            {
                spawnCDClock = 0f;
                activeSpawning = false;
            }
            


        }        

    }

    int[] WhatToSpawn(int currentWave)
    {
        int[] retornavel;

        switch (currentWave)
        {
            case 1:
                retornavel = new int[3] { 0, 0, 0 };
                break;

            case 2:
                retornavel = new int[4] { 3, 0, 3, 0 };
                break;

            case 3:
                retornavel = new int[4] { 0, 0, 0, 1 };
                break;

            case 4:
                retornavel = new int[4] { 4, 4, 4, 4 };
                break;

            case 5:
                retornavel = new int[6] {1, 0, 0, 0, 0 ,2};
                break;

            case 6:
                retornavel = new int[5] {4, 1, 4 , 1, 4};
                break;

            case 7:
                retornavel = new int[5] {3, 1, 0, 3, 2};
                break;

            default:
                retornavel = new int[1] { 0 };
                break;

        }

        return retornavel;
    }

    int EnemiesPerWave(int currentWave)
    {
        return WhatToSpawn(currentWave).Length;        
    }

    float MultiplierPerTime(float time)
    {
        float hype = hypeHolder.hype;

        if (hype < hypeHolder.minimalHype / 5)
        {
            return 1f;
        }

        else if (hype < hypeHolder.minimalHype*2 / 5)
        {
            return 1.3f;
        }

        else if (hype < hypeHolder.minimalHype * 3 / 5)
        {
            return 1.55f;
        }

        else if (hype < hypeHolder.minimalHype * 4 / 5)
        {
            return 1.8f;
        }

        else
        {
            return 2f;
        }


        /*if (time < 8.0f)
        {
            return 1f;
        }

        else if (time < 24.0f)
        {
            Debug.Log("T2");
            return 1.3f;
            
        }

        else if (time < 32.0f)
        {
            Debug.Log("T3");
            return 1.4f;
        }

        else if (time < 40.0f)
        {
            Debug.Log("T4");
            return 1.8f;
        }

        else
        {
            Debug.Log("T5");
            return 2.0f;
        }*/
    }
        
}
