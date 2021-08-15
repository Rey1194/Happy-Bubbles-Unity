using UnityEngine;

public class Enemy : MonoBehaviour
{
  //References in the editor
  [SerializeField] private float enemySpeed = 0f;
  [SerializeField] private GameObject explotionFX;
  [SerializeField] private float selfDestroyTime = 0;
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
    timeToReduce = Random.Range(2, 5);
  }
  // Update is called once per frame
  void Update()
  {
    //Call the translate method
    TranslateEnemy();
    DestroyEnemyAfterTime();
  }
  //Move the enemy
  private void TranslateEnemy() {
    //translate the enemy Arround the world
    transform.Translate(enemyDirection * enemySpeed * Time.deltaTime, Space.World);
  }
  //Stop & Reduce the live points of the enemy if is touched
  private void OnMouseDown() {
    //Call the method to slow time from the Game manager
    GameManager.instance.SlowMo();
    //Play the damage SFX
    AudioManager.instance.PlaySFX(0);
    //Call the screen shake method from the game manager
    GameManager.instance.CameraShake();
    //Instantiate the explotion VFX
    Instantiate(explotionFX, this.transform.position, this.transform.rotation);
    //Destroy the enemy
    Destroy(this.gameObject);   
  }
  //Destroy the enemy ball after time
  private void DestroyEnemyAfterTime() {
    selfDestroyTime -= Time.deltaTime;
    float timeToReduce = Random.Range(1, 5);
    if(selfDestroyTime <= 0f) {
      Instantiate(explotionFX, this.transform.position, this.transform.rotation);
      Destroy(this.gameObject);
      gameManager.ReduceTime(Mathf.RoundToInt(timeToReduce));
    }
  }
  private void OnCollisionEnter2D(Collision2D other) {
    //Check the tag of the collitions
    if (other.gameObject.tag == "Bubble") {
      //Play SFX
      AudioManager.instance.PlaySFX(2);
      //Destroy the ball
      Destroy(other.gameObject);
      //Reduce player Time
      gameManager.ReduceTime(Mathf.RoundToInt(timeToReduce));
      //Call the method to slow time from the Game manager
      GameManager.instance.SlowMo();
      //Call the screen shake method from the game manager
      GameManager.instance.CameraShake();
      //Create another bubble
      GameObject.FindObjectOfType<SpawnManager>().SpawnBalls();
    }
  }
}