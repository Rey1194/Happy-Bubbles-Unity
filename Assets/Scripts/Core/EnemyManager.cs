using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  //Edtor's variables reference
  [SerializeField] private GameObject[] enemyBalls;
  //private variables
  [SerializeField] private float enemySpawnTime;
  [SerializeField] private float enemyWaitTime;  
  // Start is called before the first frame update
  void Start()
  {
    //call the method to spawn enemies
    InvokeRepeating("SpawEnemy", enemySpawnTime, enemyWaitTime);   
  }
  //Spawn Enemies
  public void SpawEnemy() {
    //find a random position in the child objects
    Vector2 positon = this.transform.GetChild(Random.Range(0, this.transform.childCount)).transform.position;
    //Instantiate on that random position
    Instantiate(this.enemyBalls[Random.Range(0, this.enemyBalls.Length)], positon, Quaternion.identity);
  }
}