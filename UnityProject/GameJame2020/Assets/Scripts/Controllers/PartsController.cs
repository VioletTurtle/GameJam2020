using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class PartsController : MonoBehaviour
{
    public GameObject parts1, parts2, parts3, parts4, parts5;
    public GameObject map1, map2, map3, map4, map5, mapMM;
    public GameObject PC1, PC2, PC3, PC4, PC5;
    public GameObject machine;
    public Animator animate;
    MachineManager mm;
    PartsScript ps1, ps2, ps3, ps4, ps5;
    public bool pickedUp = false;
    public int hold = 0;
    int score = 0;
    bool win = false;

    public bool playerDetected = false;

    public GameObject timeController;
    TimerController tc;
    public float addTime = 0f;

    void Start()
    {
        mm = machine.GetComponent<MachineManager>();

        ps1 = parts1.GetComponent<PartsScript>();
        ps2 = parts2.GetComponent<PartsScript>();
        ps3 = parts3.GetComponent<PartsScript>();
        ps4 = parts4.GetComponent<PartsScript>();
        ps5 = parts5.GetComponent<PartsScript>();

        tc = timeController.GetComponent<TimerController>();

        ps1.isVisible = true;
        parts1.SetActive(true);
        map1.SetActive(true);
        parts2.SetActive(false);
        parts3.SetActive(false);
        parts4.SetActive(false);
        parts5.SetActive(false);
    }

    void FixedUpdate()
    {
        // Part1
        if(ps1.isVisible)
        {
            if(pickedUp)
            {
                ps1.isVisible = false;
                map1.SetActive(false);
                PC1.SetActive(true);
                mapMM.SetActive(true);
                parts1.SetActive(false);
                hold = 1;
            }
        }
        if(hold == 1)
        {
            Debug.Log("yo");
            if (playerDetected == true)
            {
                ps2.isVisible = true;
                map2.SetActive(true);
                mapMM.SetActive(false);
                PC1.SetActive(false);
                parts2.SetActive(true);
                pickedUp = false;
                tc.startPoint += addTime;
                score += 1;
                hold = 0;
                playerDetected = false;
            }

        }

        // Part2
        if (ps2.isVisible)
        {
            if (pickedUp)
            {
                ps2.isVisible = false;
                parts2.SetActive(false);
                PC2.SetActive(true);
                map2.SetActive(false);
                mapMM.SetActive(true);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (playerDetected == true)
            {
                ps3.isVisible = true;
                map3.SetActive(true);
                PC2.SetActive(false);
                mapMM.SetActive(false);
                parts3.SetActive(true);
                pickedUp = false;
                tc.startPoint += addTime;
                score += 1;
                hold = 0;
                playerDetected = false;
            }
        }

        // Part3
        if (ps3.isVisible)
        {
            if (pickedUp)
            {
                ps3.isVisible = false;
                parts3.SetActive(false);
                PC3.SetActive(true);
                map2.SetActive(false);
                mapMM.SetActive(true);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (playerDetected == true)
            {
                ps4.isVisible = true;
                map4.SetActive(true);
                PC3.SetActive(false);
                mapMM.SetActive(false);
                parts4.SetActive(true);
                pickedUp = false;
                tc.startPoint += addTime;
                score += 1;
                hold = 0;
                playerDetected = false;
            }
        }

        // Part4
        if (ps4.isVisible)
        {
            if (pickedUp)
            {
                ps4.isVisible = false;
                map2.SetActive(false);
                PC4.SetActive(true);
                mapMM.SetActive(true);
                parts4.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (playerDetected == true)
            {
                ps5.isVisible = true;
                parts5.SetActive(true);
                PC4.SetActive(false);
                map5.SetActive(true);
                mapMM.SetActive(false);
                pickedUp = false;
                tc.startPoint += addTime;
                score += 1;
                hold = 0;
                playerDetected = false;

            }
        }

        // Part5
        if (ps5.isVisible)
        {
            if (pickedUp)
            {
                ps5.isVisible = false;
                map5.SetActive(false);
                PC5.SetActive(true);
                mapMM.SetActive(true);
                parts5.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (playerDetected == true)
            {
                pickedUp = false;
                PC5.SetActive(false);
                tc.startPoint += addTime;
                score += 1;
                hold = 0;
                win = true;
                animate.SetBool("isWin",true);
            }
        }
        if(win == true)
        {
            animate.SetBool("isWin", true);
            SceneManager.LoadScene("Win");
        }
    }
}
