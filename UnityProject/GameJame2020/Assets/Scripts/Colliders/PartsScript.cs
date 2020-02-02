using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsScript : MonoBehaviour
{
    public bool isVisible = false;
    bool addsTime = true;
    bool isPickupable = true;
    public float addedTime = 0f;
    public string object_;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.gameObject.CompareTag(object_))
            {
                AddTime();
                PartsController part = GameObject.FindGameObjectWithTag("GameController").GetComponent<PartsController>();
                part.pickedUp = true;
            }
        }
    }

    void AddTime()
    {
        TimerController time = GameObject.Find("Game").GetComponent<TimerController>();
        time.gameTime += addedTime;
    }
}
