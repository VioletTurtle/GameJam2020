using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    public GameObject parts1, parts2, parts3, parts4, parts5;
    public GameObject map1, map2, map3, map4, map5;
    public GameObject machine;
    public Animator animate;
    MachineManager mm;
    PartsScript ps1, ps2, ps3, ps4, ps5;
    public bool pickedUp = false;
    public int hold = 0;
    int score = 0;
    bool win = false;

    void Start()
    {
        mm = machine.GetComponent<MachineManager>();

        ps1 = parts1.GetComponent<PartsScript>();
        ps2 = parts2.GetComponent<PartsScript>();
        ps3 = parts3.GetComponent<PartsScript>();
        ps4 = parts4.GetComponent<PartsScript>();
        ps5 = parts5.GetComponent<PartsScript>();

        ps1.isVisible = true;
        parts1.SetActive(true);
        map1.SetActive(true);
    }

    void FixedUpdate()
    {
        // Part1
        if(ps1.isVisible)
        {
            if(pickedUp)
            {
                ps1.isVisible = false;
                parts1.SetActive(false);
                hold = 1;
            }
        }
        if(hold == 1)
        {
            Debug.Log("yo");
            if (mm.playerDetected)
            {
                ps2.isVisible = true;
                map1.SetActive(false);
                map2.SetActive(true);
                parts2.SetActive(true);
                pickedUp = false;
                score += 1;
                hold = 0;
            }

        }

        // Part2
        if (ps2.isVisible)
        {
            if (pickedUp)
            {
                ps2.isVisible = false;
                parts2.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (mm.playerDetected)
            {
                ps3.isVisible = true;
                map2.SetActive(false);
                map3.SetActive(true);
                parts3.SetActive(true);
                pickedUp = false;
                score += 1;
                hold = 0;
            }
        }

        // Part3
        if (ps3.isVisible)
        {
            if (pickedUp)
            {
                ps3.isVisible = false;
                parts3.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (mm.playerDetected)
            {
                ps4.isVisible = true;
                map3.SetActive(false);
                map4.SetActive(true);
                parts4.SetActive(true);
                pickedUp = false;
                score += 1;
                hold = 0;
            }
        }

        // Part4
        if (ps4.isVisible)
        {
            if (pickedUp)
            {
                ps4.isVisible = false;
                parts4.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (mm.playerDetected)
            {
                ps5.isVisible = true;
                parts5.SetActive(true);
                map4.SetActive(false);
                map5.SetActive(true);
                pickedUp = false;
                score += 1;
                hold = 0;
            }
        }

        // Part5
        if (ps5.isVisible)
        {
            if (pickedUp)
            {
                ps5.isVisible = false;
                parts5.SetActive(false);
                hold = 1;
            }
        }
        if (hold == 1)
        {
            if (mm.playerDetected)
            {
                pickedUp = false;
                score += 1;
                hold = 0;
                win = true;
                animate.SetBool("isWin",true);
            }
        }
    }
}
