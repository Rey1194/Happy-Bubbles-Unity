using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{  
  //References in the editor
  [SerializeField] private int enemyCollideLife = 0;
  [SerializeField] private int enemyTouchLife = 0;
  [SerializeField] private float enemySpeed = 0f;
  [SerializeField] private GameObject explotionFX;
  //private variables
  private float timeToReduce = 0;
  private Vector2 enemyDirection;
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    //Play giggle SFX
    AudioManager.instance.PlaySFX(3);
    //normalize rotation
    this.enemyDirection = Random.insideUnitCircle.normalized;
    //Find the Game Manager in the scene
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //Randomizer the time to decrease between 5 and 10
    timeToReduce = Random.Range(5, 10);
  }
  // Update is called once per frame
  void Update()
  {
    //If any of the lives of the enemy are below to 0 them destroy
    if (enemyCollideLife == 0 || enemyTouchLife == 0) {
      //Destroy this enemy
      Destroy(this.gameObject);
      //Instantiate the explode effect
      Instantiate(explotionFX, this.transform.position, this.transform.rotation);
    }
    //Call the translate method
    TranslateEnemy();
  }
  //Move the enemy
  private void TranslateEnemy() {
    //translate the enemy to the nearest bubble    
    transform.Translate(enemyDirection * enemySpeed * Time.deltaTime, Space.World);
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
    if (other.gameObject.tag == "Bubble") {
      //Play SFX
      AudioManager.instance.PlaySFX(2);
      //Destroy the ball
      Destroy(other.gameObject);
      //Reduce enemy's life
      enemyCollideLife--;
      //Reduce player Time
      gameManager.ReduceTime(Mathf.RoundToInt(timeToReduce));
    }
    else {
      //the message show if collide with the bonus too -- CHECK IT
      Debug.LogError("error with the bubbles collition");
    }
  }
}