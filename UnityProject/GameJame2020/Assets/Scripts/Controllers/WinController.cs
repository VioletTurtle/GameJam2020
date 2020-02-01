using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    int counter = 1;
    private float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeCounter + (11f * 8))
        {
            timeCounter = Time.time;
            SceneManager.LoadScene("Start Menu");
        }
    }
}
