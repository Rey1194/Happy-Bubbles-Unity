using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  //References in the editor
  [SerializeField] private int enemyCollideLife = 0;
  [SerializeField] private int enemyTouchLife = 0;
  [SerializeField] private float enemySpeed = 0f;
  //private variables
  private Vector2 enemyDirection;
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    this.enemyDirection = Random.insideUnitCircle.normalized;
    //Find the Game Manager in the scene
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }
  // Update is called once per frame
  void Update()
  {
    //If any of the lives of the enemy are below to 0 them destroy
    if (enemyCollideLife == 0 || enemyTouchLife == 0) {
      Destroy(this.gameObject);
    }
    TranslateEnemy();
  }
  //Move the enemy
  private void TranslateEnemy() {
    this.transform.Translate(this.enemyDirection * this.enemySpeed * Time.deltaTime, Space.World);
  }
  //Stop & Destroy the enemy if is touched
  private void OnMouseDrag() {
    //Reduce enemy live if is touched
    enemyTouchLife--;
    //reduce it's speed
    enemySpeed = 0;
  }
  //enemy move again if not touched
  private void OnMouseExit() {
    enemySpeed = 1;
  }
  private void OnCollisionEnter2D(Collision2D other) {
    //Check the tag of the collitions
    if (other.gameObject.tag == "BigBall") {
      //Destroy the ball
      Destroy(other.gameObject);
      //Reduce enemy's life
      enemyCollideLife--;
      //Reduce player Time
      gameManager.ReduceTime(10);
    }
    else if(other.gameObject.tag == "MidBall") {
      //Destroy the ball
      Destroy(other.gameObject);
      //Reduce enemy's Life
      enemyCollideLife--;
      //Reduce player time
      gameManager.ReduceTime(6);
    }
    else if(other.gameObject.tag == "SmallBall") {
      //Destroy the ball
      Destroy(other.gameObject);
      //Reduce enemy's life
      enemyCollideLife--;
      //Reduce Player Time
      gameManager.ReduceTime(4);
    }
  }
}