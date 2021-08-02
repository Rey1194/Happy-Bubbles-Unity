using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager instance;
  //Editor Reference
  [SerializeField] private GameObject winText;
  [SerializeField] private GameObject loseText;
  [SerializeField] private Image pauseButton;
  [SerializeField] private Sprite pauseSprite;
  [SerializeField] private Sprite playSprite;
  //private variables
  public static bool isPaused = false;  

  private void Awake() {
    instance = this;
  }
  //Restar level method
  public void Restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  //pause the game
  public void PauseGame() {
    isPaused = !isPaused;
    if(isPaused) {      
      pauseButton.sprite = playSprite;
      Time.timeScale = 0;
    }
    else if(!isPaused) {
      //Time.timeScale = timeBeforePause;
      pauseButton.sprite = pauseSprite;
      Time.timeScale = 1;
    }
    else {
      Debug.Log("Error pausing the game");
    }
  }
}