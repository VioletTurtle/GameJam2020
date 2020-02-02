using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CountdownController : MonoBehaviour {

	public Text CountdownText;
	float TimePassed = 0f;
	public GameObject AgentPlayer;

	// Use this for initialization
	void Start () {

		AgentPlayer.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		TimePassed += Time.deltaTime;
		if (TimePassed >= 5.5f)
		{
			CountdownText.text = "";
			CountdownText.color = new Color (0f,1f,0f,0f);
		}
		else if (TimePassed >= 5f)
		{
			CountdownText.text = "GO!";
			CountdownText.color = new Color (0f,1f,0f,1f);
			//Let players move
			AgentPlayer.SetActive(true);
		}
		else if (TimePassed >= 4f)
		{
			CountdownText.text = "1";
			CountdownText.color = new Color (0.5f,1f,0f,1f);
		}
		else if (TimePassed >= 3f)
		{
			CountdownText.text = "2";
			CountdownText.color = new Color (1f,1f,0f,1f);
		}
		else if (TimePassed >= 2f)
		{
			CountdownText.text = "3";
			CountdownText.color = new Color (1f,.66f,0f,1f);
		}
		else if (TimePassed >= 1f)
		{
			CountdownText.text = "4";
			CountdownText.color = new Color (1f,.33f,0f,1f);
		}
	}
}
