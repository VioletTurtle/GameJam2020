using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button level02Button, level03Button;
    public Text LockButton, Level2, Level3;
    int levelPassed;
    bool locked = true;
    int count = 1;

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("LevelPassed"))
        {
            PlayerPrefs.SetInt("LevelPassed", 0);
        }

        //COMMENT NEXT LINE B4 BUILD
        //PlayerPrefs.SetInt("LevelPassed", 0);

        if (locked == true)
        {
            LockButton.text = "Unlock";
            levelPassed = PlayerPrefs.GetInt("LevelPassed");
            level02Button.interactable = false;
            Level2.color = Color.red;
            Level2.CrossFadeAlpha(0.50f, 0f, false);
            level03Button.interactable = false;
            Level3.color = Color.red;
            Level3.CrossFadeAlpha(0.50f, 0f, false);

            switch (levelPassed)
            {
                case 1:
                    level02Button.interactable = true;
                    Level2.color = Color.black;
                    Level2.CrossFadeAlpha(1, 0f, false);
                    break;
                case 2:
                    level02Button.interactable = true;
                    Level2.color = Color.black;
                    Level2.CrossFadeAlpha(1, 0f, false);
                    level03Button.interactable = true;
                    Level3.color = Color.black;
                    Level3.CrossFadeAlpha(1, 0f, false);
                    break;
            }
        }
    }

    void Update()
    {
        if (locked == false)
        {
            level02Button.interactable = true;
            Level2.color = Color.black;
            Level2.CrossFadeAlpha(1, 1.0f, false);
            level03Button.interactable = true;
            Level3.color = Color.black;
            Level3.CrossFadeAlpha(1, 1.0f, false);
        }
        if(locked == true)
        {
            level02Button.interactable = false;
            Level2.color = Color.red;
            Level2.CrossFadeAlpha(0.50f, 0f, false);
            level03Button.interactable = false;
            Level3.color = Color.red;
            Level3.CrossFadeAlpha(0.50f, 0f, false);

            switch (levelPassed)
            {
                case 1:
                    level02Button.interactable = true;
                    Level2.color = Color.black;
                    Level2.CrossFadeAlpha(1, 0f, false);
                    break;
                case 2:
                    level02Button.interactable = true;
                    Level2.color = Color.black;
                    Level2.CrossFadeAlpha(1, 0f, false);
                    level03Button.interactable = true;
                    Level3.color = Color.black;
                    Level3.CrossFadeAlpha(1, 0f, false);
                    break;
            }
        }
    }

    public void levelToLoad(int level)
    {
        if (level == 1)
        {
            LevelController.Instance.LoadLevel("Level 1");
        }
        else if (level == 2)
        {
            LevelController.Instance.LoadLevel("Level 2");
        }
        else if (level == 3)
        {
            LevelController.Instance.LoadLevel("Level 3");
        }
    }

    public void ReturnToMainMenu()
    {
        LevelSelectBackManager.playAnimation = false;
        SceneManager.LoadScene("Start Menu");
    }

    public void resetPlayerPrefs()
    {
        level02Button.interactable = false;
        level03Button.interactable = false;
        PlayerPrefs.DeleteAll();
    }

    public void UnLock()
    {
        if (count == 1)
        {
            locked = false;
            count = 2;
            LockButton.text = "Lock";
        }
        else if (count == 2)
        {
            locked = true;
            count = 1;
            LockButton.text = "Unlock";
        }
    }
}
