using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTime : MonoBehaviour
{
  //Editor's reference
  [SerializeField] GameObject explotionVFX;
  //private variables
  private float bonusTime;
  private GameManager gameManager;

  // Start is called before the first frame update
  void Start()
  {
    //find the gameManager script component
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //give a random value to increase time between 3-10
    bonusTime = Random.Range(3, 10);
    //Play sound Effect
    AudioManager.instance.PlaySFX(1);
  }
  //If is touched, add extra time
  private void OnMouseDown() {
    //Round to a integer value to avoid problems
    gameManager.AddTime(Mathf.RoundToInt(bonusTime));
    //Instantiate the esplotion VFX
    Instantiate(explotionVFX, this.transform.position, this.transform.rotation);
    //play the sfx sound
    AudioManager.instance.PlaySFX(0);
    //Destroy the bonus object
    Destroy(this.gameObject);
  }
  //Reduce time if collide with the enemy
  private void OnCollisionEnter2D(Collision2D other) {
    //If collide with an enemy
    if(other.gameObject.tag == "Enemy") {
      //Instantiate the esplotion VFX
      Instantiate(explotionVFX, this.transform.position, this.transform.rotation);
      //play the sfx sound
      AudioManager.instance.PlaySFX(2);
      //Destroy this game object
      Destroy(this.gameObject);
      //reduce time as punish
      gameManager.ReduceTime(Mathf.RoundToInt(bonusTime));
    }
  }
}