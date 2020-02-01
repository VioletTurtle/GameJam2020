using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the collider
        if (other.GetComponent<PlayerController>() != null)
        {
            //kill the player
            StaticPlayerController.Instance.RemoveHealthFromPlayer(int.MaxValue);
        }
    }
}

