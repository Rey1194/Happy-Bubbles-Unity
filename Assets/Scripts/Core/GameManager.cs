﻿using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  //Convert to singleton
  public static GameManager instance;
  //References in the editor
  [SerializeField] private GameObject restartPanel;
  [SerializeField] private GameObject pauseButton;
  [SerializeField] private Animator camAnime;
  [SerializeField] private Animator timeAnime;
  [SerializeField] private Text timeText;
  [SerializeField] private float timeRemaining = 0;
  [SerializeField] private float slowDownValue = 0.5f;
  [SerializeField] private float slowDownLenght = 2f;
  //private variables
  private bool timeIsRunning = false;

  private void Awake() {
    instance = this;
  }
  private void Start() {
    //Start Playing the level music
    AudioManager.instance.PlaylevelMusic();
    //start counting down the time
    timeIsRunning = true;
    //start the game paused
    UIManager.instance.PauseGame();
  }
  private void Update() {
    //Call the method to reduce time
    CountDown();
    //Che if the game is paused, if not, increase the time scale
    if (UIManager.isPaused == false) {
      //increase the time scale
      Time.timeScale += (1f / slowDownLenght) * Time.unscaledDeltaTime;
      //Clamp the time scale to avoid suprassing the 1f value
      Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
  }
  //method to increase time
  public void AddTime(float addTime) {
    this.timeRemaining += addTime;
    timeAnime.SetTrigger("CanvasTimeAdd");
  }
  //method to reduce time
  public void ReduceTime(float decreaseTime) {
    this.timeRemaining -= decreaseTime;
    timeAnime.SetTrigger("CanvasTimeLose");
  }
  //method slow the time
  public void SlowMo() {
    //change the default value of time scale to slow the time
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
    //If the time is running
    if (timeIsRunning == true) {
      //check if the time is greater than 0
      if(timeRemaining > 0) {
        //if is, reduce the time
        timeRemaining -= Time.deltaTime;
        //show the remaining time in the UI
        timeText.text = timeRemaining.ToString("0");
      }
      else {
        //stop the timeScale
        Time.timeScale = 0;
        //change the text to 0
        timeText.text = "0";
        //show the restar panel UI
        restartPanel.SetActive(true);
        //deactivate the play/pause button
        pauseButton.SetActive(false);
        //Deactivate the spawn manager
        GameObject.Find("SpawnManager").SetActive(false);
        //Deactivate the enemy spawner
          //GameObject.Find("EnemyManager").SetActive(false);
          GameObject.Find("SpawnWaveManager").SetActive(false);
      }
    }
  }
}