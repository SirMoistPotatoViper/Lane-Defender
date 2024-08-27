/*****************************************************************************
// File Name : Spawner.cs
// Author : Jake Slutzky
// Creation Date : August 23, 2024
//
// Brief Description : This script spawns enemies in from the various spawn spots between the lanes
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// The Variables that the Spawner script refernces
    /// </summary>
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnSpots;
    [SerializeField] private float spawnTime;
    private bool canSpawnEnemy = true;

    /// <summary>
    /// On start, initate the "enemySpawn" IEnumerator
    /// </summary>
    void Start()
    {
        enemySpawn();
    }

    /// <summary>
    /// Every update frame, start "spawnEnemy" if "canSpawnEnemy" is true
    /// </summary>
    void Update()
    {
        if (canSpawnEnemy == true)
        {
            spawnEnemy();
        }
    }

    /// <summary>
    /// set canSpawnEnemy to false, pick a random transform from the spawn spots list, set the spawner's position to said spot,
    /// pick a random enemy among the enemies list, spawn said enemy, then start the "enemySpawn" coroutine
    /// </summary>
    public void spawnEnemy()
    {
        canSpawnEnemy = false;
        int spawnSpot = Random.Range(0, spawnSpots.Length);
        transform.position = spawnSpots[spawnSpot].position;
        int enemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[enemy], transform.position, Quaternion.identity);
        StartCoroutine(enemySpawn());
        //yield return new WaitForSeconds(spawnTime);
        //canSpawnEnemy = true;
        //transform.position == spawnSpot.transform.position;
    }

    /// <summary>
    /// wait for "spawnTime", then set "canSpawnEnemy" to true
    /// </summary>
    private IEnumerator enemySpawn()
    {
        yield return new WaitForSeconds(spawnTime);
        canSpawnEnemy = true;
    }
}
