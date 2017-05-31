﻿using System.Collections;
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
					instance.GetComponent<EnemyHealth>().initialSpeed *= MultiplierPerTime(totalTime);
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
                retornavel = new int[4] { 0, 0, 0, 0 };
                break;

            case 3:
                retornavel = new int[4] { 0, 0, 0, 1 };
                break;

            case 4:
                retornavel = new int[4] { 0, 1, 1, 0 };
                break;

            case 5:
                retornavel = new int[5] { 0, 0, 0, 0 ,1};
                break;

            case 6:
                retornavel = new int[6] { 2, 0, 1, 0 , 1, 0};
                break;

            case 7:
                retornavel = new int[4] {0, 1, 0, 1 };
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

       /* switch (currentWave)
        {
            case 1:
                return 3;

            case 2:
                return 4;

            case 3:
                return 4;

            case 4:
                return 5;

            case 5:
                return 6;

            default:
                if (currentWave < 10)
                {
                    return 6;
                }
                else
                {
                    return (7 + Mathf.FloorToInt(((currentWave - 10) / 6)));
                }
        }*/
    }

    float MultiplierPerTime(float time)
    {
        if (time < 8.0f)
        {
            return 0.8f;
        }

        else if (time < 16.0f)
        {
            return 1.7f;
        }

        else if (time < 24.0f)
        {
            return 1.8f;
        }

        else if (time < 32.0f)
        {
            return 2.0f;
        }

        else
        {
            return 2.9f;
        }
    }
        
}
