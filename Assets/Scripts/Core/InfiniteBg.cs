using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBg : MonoBehaviour
{
  //Editor's reference
  [SerializeField] private Vector3 posToBack;
  [SerializeField] private Vector3 destination;
  [SerializeField] private float speed = 0;

  // Update is called once per frame
  void Update()
  {
    //If the game is paused return code
    if(Time.timeScale == 0)
      return;
    //move the sprite to the destination
    transform.position = Vector2.MoveTowards(transform.position, destination, speed);
    //if the sprite is on the destination
    if (transform.position == destination) {
      //return to it's original position
      transform.position = posToBack;
    }
  }
}
