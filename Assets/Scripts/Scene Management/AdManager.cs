using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
  #if UNITY_IOS
    string gameId = "4257276";
  #else
    string gameId = "4257277";
  #endif

  // Start is called before the first frame update
  void Start()
  {
    Advertisement.Initialize(gameId);
  }

  public void PlayAd() {
    if(Advertisement.IsReady("Interstitial_Android")) {
      Advertisement.Show("Interstitial_Android");
    }
  }
}