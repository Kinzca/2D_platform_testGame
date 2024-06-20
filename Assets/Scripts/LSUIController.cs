using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBack, fadeFromBack;

    public GameObject levelInfoPanel;

    public Text levelName;
    public Text gemFound, gemTotal;
    public Text timeBest, timeTarget;

    public static LSUIController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FadeFromBack();

    }

    // Update is called once per frame
    void Update()
    {

        if (fadeToBack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBack = false;
            }
        }
        if (fadeFromBack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeFromBack = false;
            }
        }
    }

    public void FadeToBack()
    {
        fadeToBack = true;
        fadeFromBack = false;
    }

    public void FadeFromBack()
    {
        fadeFromBack = true;
        fadeToBack = false;
    }

    public void ShowInfo(MainPoint levelInfo)
    {
        levelName.text = levelInfo.levelName;


        if (levelInfo.bestTime == 0)
        {
            timeBest.text = "BEST:---";
        }
        else
        {
            timeBest.text = "BEST:" + levelInfo.bestTime.ToString("F2");
        }
        timeTarget.text = "TARGET:" + levelInfo.targetTime + "s";

        gemFound.text = "FOUND:" + levelInfo.gemFound;
        gemTotal.text = "IN LEVEL" + levelInfo.gemTotal;

        levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
