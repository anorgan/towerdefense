using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName = "Wave";
        public float spawnRate = 1f;
        public Transform spawnPoint;
        public Transform[] enemies;
    }

    public Wave[] waves;
    public float timeBetweenWaves = 15f;

    private float countDown = 2f;
    private int waveIndex = 0;

	
	// Update is called once per frame
	void Update ()
    {
        if (waves.Length < waveIndex + 1)
        {
            Debug.Log("No more waves");
        } else if (countDown <= 0f) {
            Debug.Log("Spawning wave " + waveIndex.ToString() + " of " + waves.Length.ToString());
            SpawnWave(waves[waveIndex]);
            countDown = timeBetweenWaves;
            waveIndex++;
        }

        countDown -= Time.deltaTime;	    
	}

    void SpawnWave(Wave wave)
    {
        // Get this wave's spawn point and enemies
        Transform spawnPoint = wave.spawnPoint;
        Transform[] enemies = wave.enemies;
        float spawnRate = 1f; // wave.spawnRate;

        // Spawn enemies of this wave
        Debug.Log("Starting coroutine to spawn enemies");
        StartCoroutine(SpawnEnemy(spawnPoint, enemies, spawnRate));
    }

    IEnumerator SpawnEnemy(Transform spawnPoint, Transform[] enemies, float spawnRate)
    {
        foreach (Transform enemy in enemies)
        {
            Debug.Log("Spawning enemy");
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
