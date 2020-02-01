using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindsManager : MonoBehaviour
{
    // Script from Studica.com

    //Used for singleton
    public static KeyBindsManager KBM;

    //Create Keycodes that will be associated with each of our commands.
    //These can be accessed by any other script in our game
    public KeyCode Jump { get; set; }
    //public KeyCode forward { get; set; }
    //public KeyCode backward { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }
    // Added Keycodes for switching characters and attacking
    public KeyCode CSwitch { get; set; }
    public KeyCode Attack { get; set; }

    void Awake()
    {
        //Singleton pattern
        if (KBM == null)
        {
            DontDestroyOnLoad(gameObject);
            KBM = this;
        }
        else if (KBM != this)
        {
            Destroy(gameObject);
        }
        /*Assign each keycode when the game starts.
         * Loads data from PlayerPrefs so if a user quits the game,
         * their bindings are loaded next time. Default values
         * are assigned to each Keycode via the second parameter
         * of the GetString() function
         */
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "W"));
        //forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        //backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey", "A"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey", "D"));
        CSwitch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwitchKey", "C"));
        Attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackKey", "J"));

        // NOTE: Next edit the player conrtoller to work with this script
    }

}
