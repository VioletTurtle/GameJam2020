using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    [SerializeField] public float gameTime = 180f;
    [SerializeField] private float MaxTime = 180f;
    [SerializeField] private float startCountDown = 5f;
    private float startPoint;
    private bool playing;
    private bool started;

    // Use this for initialization
    private void Start ()
    {
        Time.timeScale = 1f;
        started = false;
        playing = true;
		timerText.text = "180";
        startPoint = Time.realtimeSinceStartup;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        float now = float.NegativeInfinity;

        if (!started)
        {
            now = Time.realtimeSinceStartup - startPoint;

            if (now > startCountDown)
            {
                started = true;
                Time.timeScale = 1f;
                startPoint = Time.time;
            }
            else
            {
                timerText.text = (startCountDown - now).ToString("000");
            }
        }
        else
        {
            if (playing)
            {
                now = Time.time - startPoint;

                timerText.text = (gameTime - now).ToString("000");
            }


            if (!playing || now >= gameTime)
            {
                playing = false;
                timerText.text = 0.ToString("000");
                //End Death
            }
        }
	}
}
