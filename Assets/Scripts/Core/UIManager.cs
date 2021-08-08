using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public static UIManager instance;
  //Editor Reference  
  [SerializeField] private GameObject pausePanel;  
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
      pausePanel.SetActive(true);
    }
    else if(!isPaused) {      
      pauseButton.sprite = pauseSprite;
      Time.timeScale = 1;
      pausePanel.SetActive(false);
    }
    else {
      Debug.Log("Error pausing the game");
    }
  }
}