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
  [SerializeField] private GameObject explotionFX;
  //Editor variables
  [SerializeField] private float moveSpeed = 0;
  [SerializeField] private float selfDestroyTime = 0;
  [SerializeField] private float changeDirectionTime = 0;
  //private variables
  private float rotateSpeed;
  private float saveDirectionTime;
  private float localSpeed;
  private Vector2 moveDirection;
  private GameManager gameManager;
  [Range(0, 100)] public float chanceToDrop;

  private void Start() {
    //find the Game Manager in the scene
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //save the move speed to use for moving the ball again
    this.localSpeed = moveSpeed;
    //Give a random rotate speed between 30 - 60
    this.rotateSpeed = Random.Range(30f, 60f);
    //Give it to the ball a random direction
    ChangeDirection();
    //Save the time to change the direction
    saveDirectionTime = changeDirectionTime;
  }
  private void FixedUpdate() {
    //rotate the ball
    this.RotateBall();
    //Destroy the ball after time
    this.DestroyBallAfterTime();
    //traslate the ball
    this.TranslateBall();
  }
  private void Update() {
    //Check if the direction time is greater than 0
    if(changeDirectionTime > 0) {
      //If is reduce it after time
      changeDirectionTime -= Time.deltaTime;
    }
    //If not
    else {
      //Call the function to change the direction
      ChangeDirection();
      //set the original value
      changeDirectionTime = saveDirectionTime;
    }
  }
  //In the ball is touched, destroy it
  private void OnMouseDown() {
    //Plays sound effect
    AudioManager.instance.PlaySFX(0);
    //Call the method to destroy the ball
      DestroyBall();
  }
  
  //Rotate te ball around itself
  private void RotateBall() {
    this.transform.Rotate(0f, 0f, this.rotateSpeed * Time.deltaTime, Space.Self);
  }
  //translate the ball arround the scene
  private void TranslateBall() {
    this.transform.Translate(this.moveDirection * this.moveSpeed * Time.deltaTime, Space.World);
  }
  //Change the direction of the ball
  private void ChangeDirection() {
    //Returns a random point inside or on a circle with radius 1.0
    this.moveDirection = Random.insideUnitCircle.normalized;
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
      //Instantiate the explotion particle effect
      Instantiate(explotionFX, this.transform.position, this.transform.rotation);
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
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        //destroy the ball
        Destroy(this.gameObject);
        //instantiate 2 mid size balls
        //Instantiate(this.midBall, this.transform.position, Quaternion.identity);
        //Instantiate(this.midBall, this.transform.position, Quaternion.identity);
        
        //Instantiate the explotion particle effect
        Instantiate(explotionFX, this.transform.position, this.transform.rotation);
        //Instantiate the Adding bonus time
        ChanceToDrop();
        break;
      //If is mid size
    case BallSize.mid:
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        //Destroy the ball
        Destroy(this.gameObject);
        //Instantiate 2 small balls
        //Instantiate(this.smallBall, this.transform.position, Quaternion.identity);
        //Instantiate(this.smallBall, this.transform.position, Quaternion.identity);
        
        //Instantiate the explotion particle effect
        Instantiate(explotionFX,this.transform.position, this.transform.rotation);
        //Instantiate the Adding bonus time
        ChanceToDrop();
        break;
      //If if a small ball
    case BallSize.small:
        GameObject.Find("SpawnWaveManager").GetComponent<SpawnWaveManager>().EnemyDestroyed();
        //destroy the ball
        Destroy(this.gameObject);
        //Instantiate the explotion particle effect
        Instantiate(explotionFX, this.transform.position, this.transform.rotation);
        //Instantiate the Adding bonus time
        ChanceToDrop();
        //call the spawner method and create more bubbles
        //GameObject.FindObjectOfType<SpawnManager>().SpawnBalls();
        break;
      //Debug log error message
      default:
        Debug.LogError("Error Instantiating balls");
        break;
    }
  }
}