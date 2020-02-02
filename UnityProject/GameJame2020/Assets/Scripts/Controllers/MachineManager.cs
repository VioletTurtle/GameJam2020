using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    PartsController parts;
    int score = 0;
    public bool playerDetected = false;
    private void Start()
    {
        parts = GameObject.FindGameObjectWithTag("GameController").GetComponent<PartsController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(parts.hold == 1)
            {
                playerDetected = true;
                parts.hold = 0;
            }
        }
    }
}
