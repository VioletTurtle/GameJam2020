using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    private Text timerText;
    [SerializeField] private float gameTime = 60f;
    [SerializeField] private float startCountDown = 5f;
    private float startPoint;
    [SerializeField] private ScoreController score;
    private bool playing;
    private bool started;

    // Use this for initialization
    private void Start ()
    {
        Time.timeScale = 1f;
        started = false;
        playing = true;
        timerText = GetComponentInChildren<Text>();
		timerText.text = "60";
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


                // here is where u can add stuff to start the game
                // uwu



            }
            else
            {
                //timerText.text = (startCountDown - now).ToString("0.00");
            }
        }
        else
        {
            if (playing)
            {
                now = Time.time - startPoint;

                timerText.text = (gameTime - now).ToString("00.00");
            }


            if (!playing || now >= gameTime)
            {
                playing = false;
                timerText.text = 0.ToString("00.00");
                //score.Winner();
                if (Input.anyKeyDown)
                {
                    //Debug.Log("uwu");
                    SceneManager.LoadScene("StarMenu");
                }
            }
        }
	}
}
