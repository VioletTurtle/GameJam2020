using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KB_MenuScript : MonoBehaviour
{
    Transform KBM_Panel;    //Key binding manager panel

    Event keyEvent;

    Text buttonText;

    KeyCode newKey;

    bool waitingForKey;

    public GameObject HUD;

    void Start()
    {

        //Assign menuPanel to the Panel object in our Canvas

        //Make sure it's not active when the game starts

        KBM_Panel = transform.Find("KB_Panel");

        KBM_Panel.gameObject.SetActive(false);

        waitingForKey = false;



        /*iterate through each child of the panel and check

         * the names of each one. Each if statement will

         * set each button's text component to display

         * the name of the key that is associated

         * with each command. Example: the ForwardKey

         * button will display "W" in the middle of it

         */

        for (int i = 0; i < KBM_Panel.childCount; i++)
        {

            if (KBM_Panel.GetChild(i).name == "JumpKey")

                KBM_Panel.GetChild(i).GetComponentInChildren<Text>().text = KeyBindsManager.KBM.Jump.ToString();

            else if (KBM_Panel.GetChild(i).name == "LeftKey")

                KBM_Panel.GetChild(i).GetComponentInChildren<Text>().text = KeyBindsManager.KBM.Left.ToString();

            else if (KBM_Panel.GetChild(i).name == "RightKey")

                KBM_Panel.GetChild(i).GetComponentInChildren<Text>().text = KeyBindsManager.KBM.Right.ToString();

            else if (KBM_Panel.GetChild(i).name == "SwitchKey")

                KBM_Panel.GetChild(i).GetComponentInChildren<Text>().text = KeyBindsManager.KBM.CSwitch.ToString();

            else if (KBM_Panel.GetChild(i).name == "AttackKey")

                KBM_Panel.GetChild(i).GetComponentInChildren<Text>().text = KeyBindsManager.KBM.Attack.ToString();

        }
    }





    void Update()
    {
        //Escape key will open or close the panel

        if (Input.GetKeyDown(KeyCode.Escape) && !KBM_Panel.gameObject.activeSelf)
        {
            KBM_Panel.gameObject.SetActive(true);
            HUD.SetActive(false);
            Time.timeScale = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && KBM_Panel.gameObject.activeSelf)
        {
            KBM_Panel.gameObject.SetActive(false);
            HUD.SetActive(true);
            Time.timeScale = 1f;
        }
    }



    void OnGUI()
    {

        /*keyEvent dictates what key our user presses

         * bt using Event.current to detect the current

         * event

         */

        keyEvent = Event.current;

        //Executes if a button gets pressed and

        //the user presses a key

        if (keyEvent.isKey && waitingForKey)
        {

            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses

            waitingForKey = false;

        }

    }

    /*Buttons cannot call on Coroutines via OnClick().

     * Instead, we have it call StartAssignment, which will

     * call a coroutine in this script instead, only if we

     * are not already waiting for a key to be pressed.

     */

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)

            StartCoroutine(AssignKey(keyName));
    }


    //Assigns buttonText to the text component of

    //the button that was pressed

    public void SendText(Text text)
    {
        buttonText = text;
    }


    //Used for controlling the flow of our below Coroutine

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)

            yield return null;
    }


    /*AssignKey takes a keyName as a parameter. The

     * keyName is checked in a switch statement. Each

     * case assigns the command that keyName represents

     * to the new key that the user presses, which is grabbed

     * in the OnGUI() function, above.

     */

    public IEnumerator AssignKey(string keyName)
    {

        waitingForKey = true;

        yield return WaitForKey(); //Executes endlessly until user presses a key


        switch (keyName)

        {

            /*case "forward":

                GameManager.GM.forward = newKey; //Set forward to new keycode

                buttonText.text = GameManager.GM.forward.ToString(); //Set button text to new key

                PlayerPrefs.SetString("forwardKey", GameManager.GM.forward.ToString()); //save new key to PlayerPrefs

                break;

            case "backward":

                GameManager.GM.backward = newKey; //set backward to new keycode

                buttonText.text = GameManager.GM.backward.ToString(); //set button text to new key

                PlayerPrefs.SetString("backwardKey", GameManager.GM.backward.ToString()); //save new key to PlayerPrefs

                break;*/

            case "Jump":

                KeyBindsManager.KBM.Jump = newKey; //set jump to new keycode

                buttonText.text = KeyBindsManager.KBM.Jump.ToString(); //set button text to new key

                PlayerPrefs.SetString("JumpKey", KeyBindsManager.KBM.Jump.ToString()); //save new key to playerprefs

                break;

            case "Left":

                KeyBindsManager.KBM.Left = newKey; //set left to new keycode

                buttonText.text = KeyBindsManager.KBM.Left.ToString(); //set button text to new key

                PlayerPrefs.SetString("LeftKey", KeyBindsManager.KBM.Left.ToString()); //save new key to playerprefs

                break;

            case "Right":

                KeyBindsManager.KBM.Right = newKey; //set right to new keycode

                buttonText.text = KeyBindsManager.KBM.Right.ToString(); //set button text to new key

                PlayerPrefs.SetString("RightKey", KeyBindsManager.KBM.Right.ToString()); //save new key to playerprefs

                break;

            case "CSwitch":

                KeyBindsManager.KBM.CSwitch = newKey; //set right to new keycode

                buttonText.text = KeyBindsManager.KBM.CSwitch.ToString(); //set button text to new key

                PlayerPrefs.SetString("SwitchKey", KeyBindsManager.KBM.CSwitch.ToString()); //save new key to playerprefs

                break;

            case "Attack":

                KeyBindsManager.KBM.Attack = newKey; //set right to new keycode

                buttonText.text = KeyBindsManager.KBM.Attack.ToString(); //set button text to new key

                PlayerPrefs.SetString("AttackKey", KeyBindsManager.KBM.Attack.ToString()); //save new key to playerprefs

                break;

        }

        yield return null;

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        LevelSelectBackManager.playAnimation = false;
        LevelController.Instance.LoadLevel("Start Menu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

}
