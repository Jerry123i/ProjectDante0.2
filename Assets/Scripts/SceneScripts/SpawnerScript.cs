using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject enemy;

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
        }

        if (activeSpawning)
        {
            if (nToSpawn > 0)
            {
                if (spawnCDClock>= 1.0f)
                {
                    instance = Instantiate(enemy, spawners[Random.Range(0, spawners.Length)].transform.position, Quaternion.Euler(Vector3.zero));
                    instance.GetComponent<EnemyController>().moveSpeed *= MultiplierPerTime(totalTime);
                    spawnCDClock = 0f;
                    nToSpawn--;
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

    int EnemiesPerWave(int currentWave)
    {
        switch (currentWave)
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
        }
    }

    float MultiplierPerTime(float time)
    {
        if (time < 8.0f)
        {
            return 0.8f;
        }

        else if (time < 16.0f)
        {
            return 1.0f;
        }

        else if (time < 24.0f)
        {
            return 1.3f;
        }

        else if (time < 32.0f)
        {
            return 1.5f;
        }

        else
        {
            return 1.8f;
        }
    }
        
}
