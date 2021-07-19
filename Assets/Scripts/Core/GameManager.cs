using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  //References in the editor
  [SerializeField] private GameObject winText;
  [SerializeField] private GameObject loseText;
  [SerializeField] private Text timeText;
  [SerializeField] private float timeRemaining = 0;
  //private variables
  private bool timeIsRunning = false;

  private void Start() {
    //Start Playing the level music
    AudioManager.instance.PlaylevelMusic();
    //start counting down the time
    timeIsRunning = true;    
  }
  private void Update() {
    CountDown();
  }
  //Restar button
  public void Restart() {
    SceneManager.LoadScene("Samplelevel");
  }
  //Adding time if a ball is detroyed
  public void AddTime(float addTime) {
    this.timeRemaining += addTime;
  }
  //Reduce Time if ball colide with Other ball
  public void ReduceTime(float decreaseTime) {
    this.timeRemaining -= decreaseTime;
  }
  //Timer
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