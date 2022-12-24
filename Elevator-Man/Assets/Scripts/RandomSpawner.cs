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
    public GameObject portalAnimation;
    public void EnemySpawn(int enemyAmount)
    {
        for(int i = 0; i < enemyAmount; i++)
        {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);

        GameObject portalObject = Instantiate(portalAnimation, spawnPoints[randSpawnPoint].position, transform.rotation);
        Destroy(portalObject, 2.15f);

        StartCoroutine(EnemySpawnExtra(randEnemy, randSpawnPoint));

        StartCoroutine(EnemySpawnedTrue());
        }
    }

    IEnumerator EnemySpawnExtra(int randEnemy, int randSpawnPoint)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject childObject = Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation); //as GameObject;
        childObject.transform.parent = enemies.transform; 
    }

    IEnumerator EnemySpawnedTrue()
    {
        yield return new WaitForSeconds(3.0f);
        GameManager.enemySpawned = true;
    }



}
