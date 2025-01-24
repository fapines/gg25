using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform spawnPoint; // Where the enemy will spawn
    public float spawnDelay = 2f; // Delay between enemy spawns

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay); // Spawn enemies repeatedly
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn the enemy at spawnPoint
    }
}
