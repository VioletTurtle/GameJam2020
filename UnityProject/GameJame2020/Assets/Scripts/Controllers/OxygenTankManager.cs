using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankManager : MonoBehaviour
{
    bool isVisible = true;
    bool addsTime = true;
    bool isPickupable = true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(isVisible)
        {
            if(collision.gameObject)
            {

            }
        }
    }
}
