using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public Text timerText;
    public float now;
    [SerializeField] public float gameTime = 0f;
    [SerializeField] private float MaxTime = 180f;
    [SerializeField] private float startCountDown = 5f;
    public float startPoint;
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

        if (!started)
        {
            gameTime = Time.realtimeSinceStartup - startPoint;

            if (gameTime > startCountDown)
            {
                started = true;
                Time.timeScale = 1f;
                startPoint = Time.time;
            }
            else
            {
                timerText.text = (startCountDown).ToString("000");
            }
        }
        else
        {
            if (playing)
            {
                gameTime = Time.time - startPoint;

                timerText.text = (MaxTime - gameTime).ToString("000");
            }


            if (!playing || gameTime >= MaxTime)
            {
                playing = false;
                timerText.text = 0.ToString("000");
                //End Death
            }
        }
	}
}
