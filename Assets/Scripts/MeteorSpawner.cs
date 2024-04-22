using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeteorSpawner : MonoBehaviour
{
    public GameObject meteorPrefab;
    public Transform player;
    public float spawnRate = 1f; // Meteors per second
    public float spawnDistance = 10f; // Distance above the player to spawn meteors
    public float lateralRange = 5f; // Random range to the left and right of the player for spawning

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMeteor();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnMeteor()
    {
        if (player != null)
        {
            // Random position around the player
            Vector3 spawnPosition = player.position + Vector3.up * spawnDistance;
            spawnPosition.x += Random.Range(-lateralRange, lateralRange);

            Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);
        }
    }
}