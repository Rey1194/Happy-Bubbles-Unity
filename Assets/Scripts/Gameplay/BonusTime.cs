using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{
  //private variables
  private float bonusTime;
  private GameManager gameManager;
  // Start is called before the first frame update
  void Start()
  {
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    bonusTime = Random.Range(3, 8);
  }
  //If is touched, add extra time
  private void OnMouseDown() {
    //Round to a integer value to avoid problems
    gameManager.AddTime(Mathf.RoundToInt(bonusTime));
    //Destroy the bonus object
    Destroy(this.gameObject);
  }
  //Reduce time if collide with the enemy
  private void OnCollisionEnter2D(Collision2D other) {
    //If collide with an enemy
    if(other.gameObject.tag == "Enemy") {
      //Destroy this game object
      Destroy(this.gameObject);
      //reduce time as punish
      gameManager.ReduceTime(Mathf.RoundToInt(bonusTime));
    }
  }
}
