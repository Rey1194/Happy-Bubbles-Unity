using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  //Editor's Reference
  [SerializeField] private GameObject[] gameBalls;
  [SerializeField] private float spawnTime = 0;
  [SerializeField] private float waitSpawn = 0;

  // Start is called before the first frame update
  void Start()
  {
    //Call the method to spawn balls
    InvokeRepeating("SpawnBalls", spawnTime, waitSpawn);    
  }
  //Spawn balls
  public void SpawnBalls() {
    //Find a random child object position 
    Vector2 position = this.transform.GetChild(Random.Range(0, this.transform.childCount)).transform.position;
    //Instantiate the ball in that random position;
    Instantiate(this.gameBalls[Random.Range(0, this.gameBalls.Length)], position, Quaternion.identity);
  }
}