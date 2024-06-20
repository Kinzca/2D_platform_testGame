using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image heart1;

    public Sprite fullHeart, halfHeart, emptyHeart;

    public Text GemText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBack, fadeFromBack;

    private void Awake()
    {
        UpdateGemCount();
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
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a, 1f,fadeSpeed*Time.deltaTime));
            if (fadeScreen.color.a==1f)
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

    public void UpdateHeartDisplay()
    {
        switch(PlayHeatlyController.instance.currentHealth)
        {
            case 2:
                heart1.sprite = fullHeart;
                break;
            case 1:
                heart1.sprite = halfHeart;
                break;
            case 0:
                heart1.sprite = emptyHeart;
                break;
            default:
                heart1.sprite = emptyHeart;
                break;
        }
    }

    public void UpdateGemCount()
    {
        GemText.text = LevelController.instance.gemscollected.ToString();
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
}
