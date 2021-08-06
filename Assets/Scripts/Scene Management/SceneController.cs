using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  //Method to play the game
  public void Play() {
    SceneManager.LoadScene("LevelScene");
  }
  //To go to settings
  public void Settings() {
    SceneManager.LoadScene("Settings");
  }

  //To exit the game
  public void OnApplicationQuit() {
    Debug.Log("Application ending after " + Time.time + " seconds");
  }
}