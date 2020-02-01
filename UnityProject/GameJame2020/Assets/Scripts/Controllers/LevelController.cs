using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//a level controller class to handle level changes
public sealed class LevelController : MonoBehaviour
{
    /*
      Quick Notes for those who want to use this class for specific methods:

      LevelController.Instance.LoadLevel("LevelName");           Loads a specific level (or menu scene), use the scene name to specify 
      LevelController.Instance.CompleteLevel();                  Level is marked as completed and will automatically start loading the next level
      
     */

    //singleton based variables
    private static LevelController _instance = null;                //a single instance for the whole class

    //game based variables
    AsyncOperation asyncLoadLevel;                                  //a loading operator to handle scene loading
    private static bool _levelLoading = true;                       //a bool to represent if a level is currently loading
    private static bool _levelInitilized = true;                    //a bool to represent if a level is finished loading
    private static string _currentLevelName;                        //name of the next level that needs to be loaded
    private static Vector3 _levelStartPosition;                     //a static vector 3 that represents the starting locaiton for a level 
    int sceneIndex, levelPassed;

    public static LevelController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelController>();
            }

            return _instance;
        }
    }

    IEnumerator LoadLevelRoutine(string newLevel)
    {
        _levelLoading = true;
        _levelInitilized = false;
        asyncLoadLevel = SceneManager.LoadSceneAsync(newLevel, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            print("Loading " + newLevel + "...");
            yield return null;
        }
        _levelLoading = false;
        _currentLevelName = newLevel;
    }

    //load any new level based on the string argument.
    public static void InitilizeNewLevel(string newLevel)
    {
        StaticPlayerController.Instance.RemoveAllPlayableCharacters();
        StaticPlayerController.Instance.AddMaxHealthToPlayer();
        StaticPlayerController.Instance.ResetPlayerScore();
        Instance.UpdateStartPosition();
        Instance.SpawnStartingCharacters(newLevel);
        _levelInitilized = true;
    }

    //update the static variable with the local one for this level
    void UpdateStartPosition()
    {
        switch (_currentLevelName)
        {
            case "Level 1":
                _levelStartPosition = GameObject.Find("Level1StartLocation").transform.position;
                break;
            case "Level 2":
                _levelStartPosition = GameObject.Find("Level2StartLocation").transform.position;
                break;
            case "Level 3":
                _levelStartPosition = GameObject.Find("Level3StartLocation").transform.position;
                break;
            default:
                _levelStartPosition = new Vector3(0,0,0);
                break;
        }
    }

    //loads the current level name
    public void ReloadLevel()
    {
        LoadLevel(_currentLevelName);
    }

    //loads the level that is specified by the string argument
    public void LoadLevel(string levelName)
    {
        _currentLevelName = SceneManager.GetActiveScene().name;
        Instance.StartCoroutine(LoadLevelRoutine(levelName));
    }

    //a method that loads the next level based on the name of the current level opened
    public void LoadNextLevel()
    {
        string nextLevelName;
        _currentLevelName = SceneManager.GetActiveScene().name;

        switch (_currentLevelName)
        {
            case "Level 1":
                nextLevelName = "Level 2";
                break;
            case "Level 2":
                nextLevelName = "Level 3";
                break;
            case "Level 3":
                nextLevelName = "Start Menu";
                break;
            default:
                nextLevelName = "Start Menu";
                break;
        }

        LoadLevel(nextLevelName);
    }

    //spawns specified starting character for the level
    void SpawnStartingCharacters(string levelName)
    {
        switch (levelName)
        {
            case "Level 1":
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.banditPlayer);
                break;
            case "Level 2":
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.banditPlayer);
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.shooterPlayer);
                break;
            case "Level 3":
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.banditPlayer);
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.shooterPlayer);
                StaticPlayerController.Instance.AddPlayableCharacter(StaticPlayerController.Instance.gothicPlayer);
                break;
            default:
                break;
        }
    }

    //return the starting position of the current level
    public Vector3 GetStartPosition()
    {
        return _levelStartPosition;
    }

    //a function for completing the current level
    public void CompleteLevel()
    {
        //add in level cinematics or messages here
        // may need to add in a timer so that it does not immediently load the next level
        if (levelPassed < sceneIndex)
        {
            PlayerPrefs.SetInt("LevelPassed", sceneIndex);
        }
        LoadNextLevel();
    }


    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Static Level Controller");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        //do not remove this, or we wont be able to load levels
        DontDestroyOnLoad(gameObject);

        //will end up deleting these once we get the menu working
        _currentLevelName = SceneManager.GetActiveScene().name;
        UpdateStartPosition();
    }

    void Start()
    {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //if the level is finished loading and has not been initilized...
        if (!_levelLoading && !_levelInitilized)
        {
            InitilizeNewLevel(_currentLevelName);
        }
    }
}
