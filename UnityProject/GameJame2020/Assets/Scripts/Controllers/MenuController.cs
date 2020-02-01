﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level01");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
