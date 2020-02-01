using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CountdownController : MonoBehaviour {

	private Text CountdownText;
	float TimePassed = 0f;
	public NavMeshAgent navAgentPlayerRed;
	public NavMeshAgent navAgentPlayerBlue;

	// Use this for initialization
	void Start () {
		CountdownText = gameObject.GetComponent<Text> ();

		navAgentPlayerRed.enabled = false;
		navAgentPlayerBlue.enabled = false;
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
			navAgentPlayerRed.enabled = true;
			navAgentPlayerBlue.enabled = true;
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
