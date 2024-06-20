using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public float waitToRespawn;

    public int gemscollected;

    public GameObject levelCompleteText;

    public string loadToScene;

    public float timeInLevel;

    public bool isrebirth;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayerCo()
    {
        isrebirth = true;
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayHeatlyController.instance.gameObject .SetActive (false);

        yield return new WaitForSeconds(waitToRespawn-(1f/UIController.instance.fadeSpeed));
        UIController.instance.FadeToBack();
        yield return new WaitForSeconds((1f / UIController.instance.fadeSpeed)+.2f);
        UIController.instance.FadeFromBack();

        PlayHeatlyController.instance.gameObject.SetActive(true);

        SoundManager.instance.PlaySFX(7);

        PlayerController.instance.transform.position = CheckPointController.instance.spawnPoint;

        PlayHeatlyController.instance.currentHealth = PlayHeatlyController.instance.maxHealth;

        UIController.instance.UpdateHeartDisplay();
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        CameraController.instance.stopFollow = true;

        PlayerController.instance.stopInput = true;

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBack();

        levelCompleteText.SetActive(true);

        yield return new WaitForSeconds((1f/UIController.instance.fadeSpeed)+1f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "unLocked", 1);

        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "gems"))
        {
            if(gemscollected>PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "gems", gemscollected);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "gems", gemscollected);
        }

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "time"))
        {
            if(timeInLevel<PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "time", timeInLevel);
        }
        UIController.instance.FadeFromBack();

        SceneManager.LoadScene(loadToScene);
    }
}
