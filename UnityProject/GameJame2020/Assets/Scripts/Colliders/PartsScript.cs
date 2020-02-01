using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsScript : Collision
{
    bool isVisible = true;
    bool addsTime = true;
    bool isPickupable = true;

    private override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.gameObject.CompareTag(object_))
            {
                AddTime();
                // Remove from parts array
                // Destroy object
            }
        }
    }

    void AddTime()
    {
        // Add time to timer
    }
}
