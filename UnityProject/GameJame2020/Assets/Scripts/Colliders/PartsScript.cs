using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsScript : MonoBehaviour
{
    public bool isVisible = false;
    bool addsTime = true;
    bool isPickupable = true;
    public float addedTime = 0f;
    PartsController part;


    void Start()
    {
        part = GameObject.FindGameObjectWithTag("GameController").GetComponent<PartsController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isVisible)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Enter Box");
                part.pickedUp = true;
            }
        }
    }

   
}
