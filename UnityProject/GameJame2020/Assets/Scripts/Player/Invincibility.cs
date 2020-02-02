using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    BoxCollider2D player;
    void Start()
    {
        TimerController time = GameObject.Find("Game").GetComponent<TimerController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }
    /*public IEnumerator Invincible()
    {

    }*/
}
