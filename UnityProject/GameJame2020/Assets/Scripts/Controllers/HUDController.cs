using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text ScoreText;
    public Text P1;
    public Text P2;
    public Text P3;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = StaticPlayerController.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = StaticPlayerController.score.ToString();
    }
}
