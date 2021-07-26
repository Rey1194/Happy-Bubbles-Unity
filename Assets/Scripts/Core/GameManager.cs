using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{  
  public static GameManager instance;                                       //Convert to singleton  
  [SerializeField] private GameObject winText;                              //References in the editor
  [SerializeField] private GameObject loseText;
  [SerializeField] private Animator camAnime;
  [SerializeField] private Text timeText;
  [SerializeField] private float timeRemaining = 0;
  [SerializeField] private float slowDownValue = 0.5f;
  [SerializeField] private float slowDownLenght = 2f;
  private bool timeIsRunning = false;                                       //private variables

  private void Awake() {
    instance = this;
  }
  private void Start() {
    AudioManager.instance.PlaylevelMusic();                                 //Start Playing the level music    
    timeIsRunning = true;                                                   //start counting down the time
  }
  private void Update() {
    //Call the method to reduce time
    CountDown();
    //increase the time scale
    Time.timeScale += (1f / slowDownLenght) * Time.unscaledDeltaTime;
    //Clamp the time scale to avoid suprassing the 1f value
    Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
  }
  //Restar level method
  public void Restart() {
    SceneManager.LoadScene("LevelScene");
  }
  //method to increase time
  public void AddTime(float addTime) {
    this.timeRemaining += addTime;
  }
  //method to reduce time
  public void ReduceTime(float decreaseTime) {
    this.timeRemaining -= decreaseTime;
  }
  //method slow the time
  public void SlowMo() {
    Time.timeScale = slowDownValue;
    Time.fixedDeltaTime = Time.timeScale * 0.02f;
  }
  //Screen shake method
  public void CameraShake() {
    //Call the animation controller and trigger the animation
    camAnime.SetTrigger("CameraShake");
  }
  //Timer method
  public void CountDown() {
    if (timeIsRunning == true) {
      if(timeRemaining > 0) {
        timeRemaining -= Time.deltaTime;
        timeText.text = timeRemaining.ToString("0");
      }
      else {
        //Lose
        Debug.Log("Game Over");
        loseText.SetActive(true);
        Time.timeScale = 0;
      }
    }
  }
}