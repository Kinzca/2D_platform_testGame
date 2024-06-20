using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;

    private MainPoint[] allPoint;
    // Start is called before the first frame update
    void Start()
    {
        allPoint = FindObjectsOfType<MainPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MainPoint point in allPoint)
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.current = point;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }

    public IEnumerator LoadLevelCo()
    {
        LSUIController.instance.FadeToBack();

        yield return new WaitForSeconds((1f/LSUIController.instance.fadeSpeed)+.25f);

        SceneManager.LoadScene(thePlayer.current.levelToLoad);
    }
}
