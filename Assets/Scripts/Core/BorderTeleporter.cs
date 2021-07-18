using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTeleporter : MonoBehaviour
{
  //Editor's reference
  [SerializeField] private bool isVertical;
  [SerializeField] private float margin = 0;

  //If an game object collide with the wall
  private void OnTriggerEnter2D(Collider2D collision) {
    //check if the wall is in horizontal pos
    if (this.isVertical == true) {
      //reduce it's vector's value
      float newPos = -collision.transform.position.y;
      //check if the pos is on a positive value
      if(newPos > 0f) {
        //give a positive new post
        newPos = newPos - margin;
      }
      else {
        //else give a negative pos
        newPos = newPos + margin;
      }
      //set the new position to the collided object
      collision.transform.position = new Vector2(collision.transform.position.x, newPos);
    }
    //if not horizontal
    else {
      //reduce it's vector value
      float newPos = -collision.transform.position.x;
      //chek if the pos is on a positive value
      if (newPos > 0f) {
        //give a positive new pos
        newPos = newPos - margin;
      }
      else {
        //give a negative new pos
        newPos = newPos + margin;
      }
      //set the new position to the collided object
      collision.transform.position = new Vector2(newPos, collision.transform.position.y);
    }
  }
}
