using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject LargeMap;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        LargeMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            if (count == 0)
            {
                LargeMap.SetActive(true);
                count = 1;
            }
            else if(count == 1)
            {
                LargeMap.SetActive(false);
                count = 0;
            }
        }
    }

    
}
