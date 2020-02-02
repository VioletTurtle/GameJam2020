using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsScript : MonoBehaviour
{
    bool isVisible = true;
    bool addsTime = false;
    bool isPickupable = false;
    public string object_;

    public float subTime = 0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.gameObject.CompareTag(object_))
            {
                SubtractTime();
                // Put player in temporary invincibility
            }
        }
    }

    void SubtractTime()
    {
        TimerController time = GameObject.Find("Game").GetComponent<TimerController>();
        time.gameTime -= subTime;
    }
}
