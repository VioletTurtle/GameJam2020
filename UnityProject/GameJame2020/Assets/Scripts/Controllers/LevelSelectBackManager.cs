using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectBackManager : MonoBehaviour
{

    #region singleton
    private static LevelSelectBackManager instance;
    private LevelSelectBackManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }
    public static LevelSelectBackManager Instance
    {
        get
        {
            if (instance == null)
            {
                new LevelSelectBackManager();
            }

            return instance;
        }
    }
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Level Select Back Manager");
        if (objects.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public static bool playAnimation = true;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Start Screen").GetComponent<Animator>().enabled = playAnimation;
    }
}
