using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaveManager : MonoBehaviour
{
    public GameObject[] gameBubbles;
    public int initialWaveSize = 5;
    public float spawnDelay = 1f;
    private int currentWaveSize;   
    private int enemiesRemaining;
    
    // Start is called before the first frame update
    void Start() {
        
        currentWaveSize = initialWaveSize;
        SpawnWave(currentWaveSize);
    }

    // Update is called once per frame
    void Update() {
        // Comprueba si todos los enemigos han sido destruidos
        if (enemiesRemaining == 0 && !IsInvoking(nameof(SpawnNextWaveWithDelay)))
        {
            // Inicia la siguiente oleada con un retraso
            Invoke(nameof(SpawnNextWaveWithDelay), spawnDelay);
        }
    }
    
    public void SpawnWave(int waveSize) {
        for (int i = 0; i < waveSize; i++)
        {
            GameObject bubblePrefab = gameBubbles[Random.Range(0, gameBubbles.Length)];
            Instantiate(bubblePrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
        enemiesRemaining = waveSize;
    }
    
    private void SpawnNextWaveWithDelay()
    {
        currentWaveSize++;
        SpawnWave(currentWaveSize);
    }
    
    public void EnemyDestroyed() {
        enemiesRemaining--;
    }
    
    private Vector2 GetRandomSpawnPosition() {
        Vector2 position = GameObject.Find("SpawnManager").transform.GetChild(Random.Range(0, this.transform.childCount)).transform.position;
        return position;
    }
}
