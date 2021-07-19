using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
  //editor reference
  [SerializeField] private AudioClip popSFX;
  [SerializeField] private BallSize ballSize;
  //Game Objects reference
  [SerializeField] private GameObject smallBall;
  [SerializeField] private GameObject midBall;
  [SerializeField] private GameObject bigBall;
  [SerializeField] private GameObject collectible;
  //Editor variables
  [SerializeField] private float moveSpeed = 0;
  [SerializeField] private float selfDestroyTime = 0;
  //private variables
  private float rotateSpeed;
  private float localSpeed;
  private Vector2 moveDirection;
  private GameManager gameManager;  
  [Range(0, 100)] public float chanceToDrop;

  private void Start() {    
    //find the Game Manager in the scene
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //save the move speed to use for moving the ball again
    this.localSpeed = moveSpeed;
    //Returns a random point inside or on a circle with radius 1.0
    this.moveDirection = Random.insideUnitCircle.normalized;
    //Give a random rotate speed between 30 - 60
    this.rotateSpeed = Random.Range(30f, 60f);
  }
  private void FixedUpdate() {
    //rotate the ball
    this.RotateBall();
    //Destroy the ball after time
    this.DestroyBallAfterTime();
    //traslate the ball
    this.TranslateBall();
  }
  //In the ball is touched, destroy it
  private void OnMouseDown() {
    //Plays sound effect
    AudioManager.instance.PlaySFX(0);
    //Call the method to destroy the ball
    DestroyBall();
  }
  //Rotate te ball around itselft
  private void RotateBall() {
    this.transform.Rotate(0f, 0f, this.rotateSpeed * Time.deltaTime, Space.Self);
  }
  //translate the ball arround the scene
  private void TranslateBall() {
    this.transform.Translate(this.moveDirection * this.moveSpeed * Time.deltaTime, Space.World);
  }
  //Destroy the ball after a certain time
  private void DestroyBallAfterTime() {
    //decreasing the selft destroy time
    selfDestroyTime -= Time.deltaTime;
    //Create a random value to discount
    float timeToReduce = Random.Range(1, 5);
    //if the time is equal or less to 0 
    if(selfDestroyTime <= 0) {
      //Destroy te ball
      Destroy(this.gameObject);
      //Reduce player's time
      gameManager.ReduceTime(Mathf.RoundToInt(timeToReduce));
    }
  }
  //Change to Spawn a bonus time object
  private void ChanceToDrop() {
    //create a random chance variable
    float dropSelect = Random.Range(0, 100f);
    //check if the chance is the same af the drop rate
    if (dropSelect <= chanceToDrop) {
      //if is equal or less, instantiate the bonus object
      Instantiate(collectible, this.transform.position, this.transform.rotation);
    }
  }
  //Destroy the balls
  private void DestroyBall() {
    //Switch between cases of the ball size
    switch (ballSize) {
      //if is big size
      case BallSize.big:
        //destroy the ball
        Destroy(this.gameObject);
        //instantiate 2 mid size balls
        Instantiate(this.midBall, this.transform.position, Quaternion.identity);
        Instantiate(this.midBall, this.transform.position, Quaternion.identity);
        //Instantiate the Adding bonus time
        ChanceToDrop();        
        break;
      //If is mid size
      case BallSize.mid:        
        //Destroy the ball
        Destroy(this.gameObject);
        //Instantiate 2 small balls
        Instantiate(this.smallBall, this.transform.position, Quaternion.identity);
        Instantiate(this.smallBall, this.transform.position, Quaternion.identity);
        //Instantiate the Adding bonus time
        ChanceToDrop();        
        break;
      //If if a small ball
      case BallSize.small:        
        //destroy the ball
        Destroy(this.gameObject);
        //Instantiate the Adding bonus time
        ChanceToDrop();        
        break;
      //Debug log error message
      default:
        Debug.LogError("Error Instantiating balls");
        break;
    }
  }
}