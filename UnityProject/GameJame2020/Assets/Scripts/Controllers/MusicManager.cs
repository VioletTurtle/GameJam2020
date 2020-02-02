using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static AudioSource MusicPlayer;
    [SerializeField] private AudioClip Menu;
    [SerializeField] private AudioClip Level1;
    [SerializeField] private AudioClip Win;
    private Slider volumeSlider;
    private Button muteButton;
    private Text muteButtonText;

    // to keep track of scene switches
    private static string previousSceneName;

    #region Singleton
    private static MusicManager _instance;
    public MusicManager()
    {
        if (_instance != null)
        {
            return;
        }
        else
        {
            _instance = this;
        }
    }
    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                new MusicManager();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicManager");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion // Singleton

    private void OnEnable()
    {
        MusicPlayer = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (muteButton != null)
        {
            muteButton.onClick.RemoveAllListeners();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    private void Start()
    {
        MusicPlayer = GetComponent<AudioSource>();
        MusicPlayer.volume = PlayerPrefs.GetFloat("Volume");
        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            MusicPlayer.mute = false;
        }
        else
        {
            MusicPlayer.mute = true;
        }

        previousSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        // if statement to prevent nullref when in a level
        if (volumeSlider != null)
        {
            PlayerPrefs.SetFloat("Volume", volumeSlider.value / volumeSlider.maxValue);
            MusicPlayer.volume = PlayerPrefs.GetFloat("Volume");
        }

        //Debug.Log("current clip: " + MusicPlayer.clip);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1f);
        }
        if (!PlayerPrefs.HasKey("Mute"))
        {
            PlayerPrefs.SetInt("Mute", 0);
        }

        if (scene.name == "Start Menu" ||
            scene.name == "Level 1")
        {
            volumeSlider = GameObject.FindGameObjectWithTag("Volume Slider").GetComponent<Slider>();
            volumeSlider.value = PlayerPrefs.GetFloat("Volume") * volumeSlider.maxValue;
            muteButton = GameObject.FindGameObjectWithTag("Mute Button").GetComponent<Button>();
            muteButton.onClick.AddListener(Mute);
            muteButtonText = GameObject.FindGameObjectWithTag("Mute Button").GetComponentInChildren<Text>();
            if (PlayerPrefs.GetInt("Mute") == 0)
            {
                muteButtonText.text = "AUDIO " + "ON";
            }
            else
            {
                muteButtonText.text = "AUDIO " + "OFF";
            }
            GameObject.FindGameObjectWithTag("Options Menu").SetActive(false);
        }

        MusicPlayer = GetComponent<AudioSource>();
        if (scene.name != previousSceneName)
        {
            if (scene.name == "Level01")
            {
                MusicPlayer.clip = Level1;
                MusicPlayer.Play();
                transform.position = new Vector3(0f, 0f, 0f);
            }
            else if (scene.name == "StartMenu")
            {
                MusicPlayer.clip = Menu;
                MusicPlayer.Play();
            }
            else if (scene.name == "Win")
            {
                MusicPlayer.clip = Win;
                MusicPlayer.Play();
            }
            previousSceneName = scene.name;
        }
    }

    private void Mute()
    {
        MusicPlayer.mute = !MusicPlayer.mute;
        PlayerPrefs.SetInt("Mute", MusicPlayer.mute ? 1 : 0);
        muteButtonText.text = "AUDIO " + (MusicPlayer.mute ? "OFF" : "ON");
    }

    public float volume
    {
        get
        {
            return MusicPlayer.volume;
        }
        set
        {
            MusicPlayer.volume = value;
        }
    }
}
