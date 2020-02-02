using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    PartsController parts;
    int score = 0;
    void Start()
    {
        parts = GameObject.FindGameObjectWithTag("GameController").GetComponent<PartsController>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (parts.hold == 1)
            {
                Debug.Log("Player Detected");
                parts.playerDetected = true;
            }
        }
    }
}
