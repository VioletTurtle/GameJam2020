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
    [SerializeField] private AudioClip Level1Boss;
    [SerializeField] private AudioClip Level2;
    [SerializeField] private AudioClip Level2Boss;
    [SerializeField] private AudioClip Level3;
    [SerializeField] private AudioClip Level3Boss;
    [SerializeField] private AudioClip Win;
    Collider2D MyCollider;
    private Slider volumeSlider;
    private Button muteButton;
    private Text muteButtonText;

    // to keep track of scene switches
    private static string previousSceneName;

    // to keep track of if playin boss music
    private static bool playingBossMusic;

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
        MyCollider = GetComponent<Collider2D>();
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
        playingBossMusic = false;
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
            scene.name == "Level 1" ||
            scene.name == "Level 2" ||
            scene.name == "Level 3")
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
        if (scene.name != previousSceneName || playingBossMusic)
        {
            if (scene.name == "Level 1")
            {
                MusicPlayer.clip = Level1;
                MusicPlayer.Play();
                MyCollider.enabled = true;
                transform.position = new Vector3(305.43f, 10.76f, 0f);
            }
            else if (scene.name == "Level 2")
            {
                MusicPlayer.clip = Level2;
                MusicPlayer.Play();
                MyCollider.enabled = true;
                transform.position = new Vector3(400.78f, -10.13f, 0f);
            }
            else if (scene.name == "Level 3")
            {
                MusicPlayer.clip = Level3;
                MusicPlayer.Play();
                MyCollider.enabled = true;
                transform.position = new Vector3(457.69f, 10.84f, 0f);
            }
            else if (scene.name == "Start Menu" && previousSceneName != "Level Select")
            {
                MusicPlayer.clip = Menu;
                MusicPlayer.Play();
                MyCollider.enabled = false;
            }
            else if (scene.name == "Win")
            {
                MusicPlayer.clip = Win;
                MusicPlayer.Play();
                MyCollider.enabled = false;
            }

            playingBossMusic = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!playingBossMusic &&  SceneManager.GetActiveScene().name == "Level 1")
            {
                MusicPlayer.clip = Level1Boss;
                MusicPlayer.Play();
                MyCollider.enabled = false;
                playingBossMusic = true;
            }

            else if (!playingBossMusic &&  SceneManager.GetActiveScene().name == "Level 2")
            {
                MusicPlayer.clip = Level2Boss;
                MusicPlayer.Play();
                MyCollider.enabled = false;
                playingBossMusic = true;
            }

            else if (!playingBossMusic &&  SceneManager.GetActiveScene().name == "Level 3")
            {
                MusicPlayer.clip = Level3Boss;
                MusicPlayer.Play();
                MyCollider.enabled = false;
                playingBossMusic = true;
            }
        }
    }

    //public void PlayBossMusic()
    //{
       
    //    if (!playingBossMusic && SceneManager.GetActiveScene().name == "Level 1")
    //    {
    //        MusicPlayer.clip = Level1Boss;
    //        MusicPlayer.Play();
    //        playingBossMusic = true;
    //    }

    //    else if (!playingBossMusic && SceneManager.GetActiveScene().name == "Level 2")
    //    {
    //        MusicPlayer.clip = Level2Boss;
    //        MusicPlayer.Play();
    //        playingBossMusic = true;
    //    }

    //    else if (!playingBossMusic && SceneManager.GetActiveScene().name == "Level 3")
    //    {
    //        MusicPlayer.clip = Level3Boss;
    //        MusicPlayer.Play();
    //        playingBossMusic = true;
    //    }

    //    Debug.Log(MusicPlayer.clip);
    //}
}
