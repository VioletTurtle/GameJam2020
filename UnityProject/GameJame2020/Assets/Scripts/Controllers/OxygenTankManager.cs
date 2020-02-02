using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankManager : MonoBehaviour
{
    public float addTime = 0f;
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
            this.gameObject.SetActive(false);
            AddTime();
        }
    }

    void AddTime()
    {
        tc.startPoint += addTime;
    }
}
