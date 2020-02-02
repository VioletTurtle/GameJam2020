using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    public GameObject parts1, parts2, parts3, parts4, parts5;
    public GameObject machine;
    MachineManager mm;
    PartsScript ps1, ps2, ps3, ps4, ps5;
    public bool pickedUp = false;
    public int hold = 0;
    int score = 0;
    bool win = false;

    void Start()
    {
        mm = GetComponent<MachineManager>();
        parts1 = GameObject.FindWithTag("Item1");
        parts2 = GameObject.FindWithTag("Item2");
        parts3 = GameObject.FindWithTag("Item3");
        parts4 = GameObject.FindWithTag("Item4");
        parts5 = GameObject.FindWithTag("Item5");

        ps1 = parts1.GetComponent<PartsScript>();
        ps2 = parts2.GetComponent<PartsScript>();
        ps3 = parts3.GetComponent<PartsScript>();
        ps4 = parts4.GetComponent<PartsScript>();
        ps5 = parts5.GetComponent<PartsScript>();

        ps1.isVisible = true;
    }

    void FixedUpdate()
    {
        // Part1
        if(ps1.isVisible)
        {
            if(pickedUp)
            {
                ps1.isVisible = false;
                hold = 1;
            }
        }
        else if(pickedUp == true)
        {
            if(mm.playerDetected)
            {
                ps2.isVisible = true;
                pickedUp = false;
                score += 1;
            }

        }

        // Part2
        if (ps2.isVisible)
        {
            if (pickedUp)
            {
                ps2.isVisible = false;
                hold = 1;
            }
        }
        else if (pickedUp == true)
        {
            if (mm.playerDetected)
            {
                ps3.isVisible = true;
                pickedUp = false;
                score += 1;
            }
        }

        // Part3
        if (ps3.isVisible)
        {
            if (pickedUp)
            {
                ps3.isVisible = false;
                hold = 1;
            }
        }
        else if (pickedUp == true)
        {
            if (mm.playerDetected)
            {
                ps4.isVisible = true;
                pickedUp = false;
                score += 1;
            }
        }

        // Part4
        if (ps4.isVisible)
        {
            if (pickedUp)
            {
                ps4.isVisible = false;
                hold = 1;
            }
        }
        else if (pickedUp == true)
        {
            if (mm.playerDetected)
            {
                ps5.isVisible = true;
                pickedUp = false;
                score += 1;
            }
        }

        // Part5
        if (ps5.isVisible)
        {
            if (pickedUp)
            {
                ps5.isVisible = false;
                hold = 1;
            }
        }
        else if (pickedUp == true)
        {
            if (mm.playerDetected)
            {
                pickedUp = false;
                score += 1;
                win = true;
            }
        }
    }
}
