using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainPoint : MonoBehaviour
{
    public static MainPoint instance;

    public MainPoint up,down, Left, Right;
    public bool isLevel,isLocked;
    public string levelToLoad;
    public string levelToCheck;
    public string levelName;

    public int gemFound, gemTotal; 
    public float bestTime, targetTime;

    public GameObject gems, clock;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "gems"))
            {
                gemFound=PlayerPrefs.GetInt(levelToLoad + "gems");
            }
            if (PlayerPrefs.HasKey(levelToLoad + "time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "time");
            }

            if (gemFound>=gemTotal)
            {
                gems.SetActive(true);
            }

            if (bestTime <= targetTime && bestTime !=0 )
            {
                clock.SetActive(true);
            }

            isLocked = true;

            if (levelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "unLocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "unLocked") == 1)//问题所在循环
                    {
                        isLocked = false;
                    }
                }
            }
        }
        if (levelToCheck == levelToLoad)
        {
            isLocked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
