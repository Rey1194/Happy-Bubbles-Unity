using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  //Convert to singleton
  public static AudioManager instance;
  //Audio Editor References array
  public AudioSource[] soundEffects;
  private void Awake() {
    instance = this;
  }
  //method to play the sound effect
  public void PlaySFX(int soundToPlay) {
    soundEffects[soundToPlay].Stop();
    soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);
    soundEffects[soundToPlay].Play();
  }
}
