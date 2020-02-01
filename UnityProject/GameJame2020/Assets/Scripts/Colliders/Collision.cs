using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField]
    bool isVisible = false;
    bool addsTime = false;
    bool isPickupable = false;
    public string object_;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible)
        {
            if (collision.gameObject.CompareTag(object_))
            {
                return;
            }
        }
    }
}
