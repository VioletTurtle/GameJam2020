using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsScript : Collision
{
    bool isVisible = true;
    bool addsTime = false;
    bool isPickupable = false;

    private override void OnTriggerEnter2D(Collider2D collision)
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
        // Subtract time to timer
    }
}
