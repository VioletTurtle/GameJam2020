using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//only one static controller can be made at a time and cannot be derived from
public sealed class StaticPlayerController : MonoBehaviour
{
    /*
    Quick Notes for those who want to use this class for specific methods:

    StaticPlayerController.Instance.RemoveHealthFromPlayer(int healthRemoved);     Command to remove a specified amount of health from the player
                                                                                    (does not go below 0 HP)       
    StaticPlayerController.Instance.AddHealthToPlayer(int healthAdded);            Command to add a specified amount of health to the player
                                                                                    (does not go above _playerMaxHealth)    
    StaticPlayerController.Instance.AddMaxHealthToPlayer();                        Command that increases health to _playerMaxHealth
    StaticPlayerController.Instance.KillPlayer();                                  Command that reduces the player's health to 0
    */

    //singleton based variables
    private static StaticPlayerController _instance = null;         //a single instance for the whole class

    //game related variables
    public static int _playerHealth { get; private set; } = 100;    //the player's total health
    public static int _playerMaxHealth { get; private set; } = 100; //the player's max health

    //character changer variables
    private static GameObject _currentCharacter;                    //the player's current character
    private static List<GameObject> _characterList;                 //a list of game objects for each player that is playable
    private static float _timePassedSinceChange;
    private static float _characterChangeCooldown = 1f;

    //character prefabs
    public GameObject banditPlayer;
    public GameObject gothicPlayer;
    public GameObject shooterPlayer;

    //to keep a handle on the death canvas to turn on upon death
    private static GameObject _deathCanvas;

    // score
    public static int score { get; private set; } = 0;

    public static StaticPlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StaticPlayerController>();
            }

            return _instance;
        }
    }


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Static Player Controller");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        //do not remove this, or we wont be able to spawn and switch characters
        DontDestroyOnLoad(gameObject);

        //add a new character list to the character list object
        _characterList = new List<GameObject>();

        //ensure we can change characters from the start
        _timePassedSinceChange = _characterChangeCooldown;

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // get handle on death canvas
        if (scene.name == "Level 1" ||
            scene.name == "Level 2" ||
            scene.name == "Level 3")
        {
            _deathCanvas = GameObject.FindGameObjectWithTag("Death Canvas");
            _deathCanvas.SetActive(false);
        }
    }

    public void ResetPlayerScore()
    {
        score = 0;
    }

    public void AddScore(int s)
    {
        score += s;
    }

    //unity update method
    private void Update()
    {
        //always try to look for a character to attach to
        if (_currentCharacter == null)
        {
            SwitchCharacter();
        }

        //ensure that the player does not spam character changing
        if (Input.GetKeyDown(KeyBindsManager.KBM.CSwitch) &&
            _timePassedSinceChange >= _characterChangeCooldown)
        {
            SwitchCharacter();
            _timePassedSinceChange = 0f;
        }

        //for keeping up with the button pressing function
        _timePassedSinceChange += Time.deltaTime;
    }

    //add a new character to the list of playable characters
    public void AddPlayableCharacter(GameObject characterAdded)
    {
        //if the passed in object has a playercontroller componenet
        if (characterAdded.GetComponent<PlayerController>() != null)
        {
            //make a copy of the game object prefab that is passed in
            GameObject newCharacter = Instantiate(characterAdded) as GameObject;
            newCharacter.SetActive(false);

            //add the character to the list
            _characterList.Add(newCharacter);
        }
    }

    //remove all of the current playable characters
    public void RemoveAllPlayableCharacters()
    {
        _characterList.Clear();
        _currentCharacter = null;
    }

    //remove character to the list of playable characters
    public void RemovePlayableCharacter(GameObject removedCharacter)
    {
        //if there is player controller attached to the new character,
        if (removedCharacter.GetComponent<PlayerController>() != null)
        {
            //loop though the character list
            foreach (GameObject character in _characterList)
            {
                //if the removed character instance is the same as the one in the list...
                if (character == removedCharacter)
                {
                    //remove that character from the playable characters
                    _characterList.Remove(character);
                }
            }
        }
    }

    //remove character to the list of playable characters (string version)
    public void RemovePlayableCharacter(string removedCharacter)
    {
        //loop though the character list
        foreach (GameObject character in _characterList)
        {
            //if the removed character instance is the same as the one in the list...
            if (character.name + "(Clone)" == removedCharacter)
            {
                //remove that character from the playable characters
                _characterList.Remove(character);
            }
        }
    }

    //replacing the actual game object with another
    private void ReplaceCurrentCharacter(GameObject replacementCharacter)
    {
        if (replacementCharacter != null)
        {

            GameObject tempObject;

            if (_currentCharacter == null)
            {
                tempObject = new GameObject();
                tempObject.transform.position = LevelController.Instance.GetStartPosition();

                replacementCharacter.transform.position = tempObject.transform.position;
                Destroy(tempObject);
            }
            else
            {
                tempObject = _currentCharacter;
                //***this is where we would put some kind of visual switching character effect
                _currentCharacter.SetActive(false);

                replacementCharacter.GetComponent<Rigidbody2D>().velocity = tempObject.GetComponent<Rigidbody2D>().velocity;
                replacementCharacter.GetComponent<PlayerController>().copyScript(tempObject.GetComponent<PlayerController>());
                replacementCharacter.transform.position = tempObject.transform.position;
            }
            replacementCharacter.SetActive(true);

            //set the current character to be the replacement
            _currentCharacter = replacementCharacter;
        }
    }

    //method for switching avaiable characters
    private void SwitchCharacter()
    {
        if (_characterList.Count <= 0)
        {
            //set the current character to null
            _currentCharacter = null;
        }
        else if (_characterList.Count == 1)
        {
            //make the current character the only one in the list
            ReplaceCurrentCharacter(_characterList[0]);
        }
        else if (_characterList.Count > 1)
        {
            int currentIndex = FindIndex(_currentCharacter, _characterList);
            //if we could not get a proper indexed value, return
            if (currentIndex == -1)
            {
                //make sure there is a character in the character list
                if (_characterList[0] != null)
                {
                    //attach the current character to the first one in the list
                    ReplaceCurrentCharacter(_characterList[0]);
                }
                else
                    //set the current character to be null
                    _currentCharacter = null;
            }
            //if the current index would exceed the number of elements...
            // attach the character to the first element
            if (_characterList.Count < currentIndex + 2)
                ReplaceCurrentCharacter(_characterList[0]);
            //otherwise attach it to the next element in the list
            else
                ReplaceCurrentCharacter(_characterList[currentIndex + 1]);
        }
    }

    //method that just returns the current character
    public GameObject GetCurrentCharacter()
    {
        return _currentCharacter;
    }

    //method that returns the list of current characters
    public List<GameObject> GetCharacterList()
    {
        return _characterList;
    }

    //method for finding the current index value in the list
    public int FindIndex(GameObject playerObj, List<GameObject> objList)
    {
        int idx = 0;
        foreach (GameObject listObject in objList)
        {
            if (listObject == playerObj)
                return idx;
            else
                idx++;
        }
        //if we could not find the item indexxed, return -1
        return -1;
    }

    public void RemoveHealthFromPlayer(int healthRemoved)
    {
        if (_playerHealth - healthRemoved <= 0)
        {
            _playerHealth = 0;
            _deathCanvas.SetActive(true);
            Invoke("KillPlayer", 3f);
        }
        else
            _playerHealth -= healthRemoved;
    }

    public void KillPlayer()
    {
        _playerHealth = 0;
        Debug.Log("The Character has died... oh well");
        LevelController.Instance.ReloadLevel();
    }

    public void AddHealthToPlayer(int healthAdded)
    {
        if (_playerHealth + healthAdded >= _playerMaxHealth)
            _playerHealth = _playerMaxHealth;
        else
            _playerHealth += healthAdded;
    }

    public void AddMaxHealthToPlayer()
    {
        _playerHealth = _playerMaxHealth;
    }
}
