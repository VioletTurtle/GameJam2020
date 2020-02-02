using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsScript : MonoBehaviour
{
    public float subTime = 0f;
    public GameObject timeController;
    TimerController tc;

    void Start()
    {
        tc = timeController.GetComponent<TimerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
       {
            Debug.Log("Hit");
           SubtractTime();
           // Put player in temporary invincibility
       }
    }

    void SubtractTime()
    {
        tc.startPoint -= subTime;
    }
}
