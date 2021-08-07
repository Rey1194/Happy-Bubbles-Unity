using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
  [SerializeField] private AudioMixer audioMixer;

  public void VolumenControl(float volumen) {
    audioMixer.SetFloat("Volumen", volumen);
  }
}