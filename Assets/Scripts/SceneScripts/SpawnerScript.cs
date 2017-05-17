using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject[] spawners;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {

        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SpawnNEnemies(4, enemy);
        }
    }

    void SpawnNEnemies(int n, GameObject enemyPrefab)
    {

        for (int i = 0; i < n; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawners[Random.Range(0, spawners.Length)].transform.position, Quaternion.Euler(0,0,0));
        }

    }
        
}
