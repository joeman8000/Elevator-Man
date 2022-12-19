using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //[SerializeField] private AIChase chaseScript;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public GameObject player;
    public GameObject enemies;
    public void EnemySpawn(int enemyAmount)
    {
        for(int i = 0; i < enemyAmount; i++)
        {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject childObject = Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation); //as GameObject;
        childObject.transform.parent = enemies.transform;
        }
    }

    //click to spawn
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
        }
    }  */
}
