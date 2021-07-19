using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  //Convert to singleton
  public static AudioManager instance;
  //Audio Editor References array
  public AudioSource[] soundEffects;
  //Audio Editor References
  public AudioSource bgm;
  private void Awake() {
    instance = this;
  }
  //method to play the sound effect
  public void PlaySFX(int soundToPlay) {
    //Stop the sound if is playing
    soundEffects[soundToPlay].Stop();
    //give a random value to the pitch's sound
    soundEffects[soundToPlay].pitch = Random.Range(0.8f, 1.2f);
    //Play the sound
    soundEffects[soundToPlay].Play();
  }
  //Play level music
  public void PlaylevelMusic() {
    bgm.Play();
  }
}
