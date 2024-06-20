using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene,continueScene;

    public GameObject continueButton, newGameButton, startButton, optoinButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (PlayerPrefs.HasKey(startScene + "unLocked"))
        {
            optoinButton.SetActive(false);
            startButton.SetActive(false);

            continueButton.SetActive(true);
            newGameButton.SetActive(true);

        }
        else
        {
            SceneManager.LoadScene(startScene);
            PlayerPrefs.DeleteAll();
        }  
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void newGame()
    {
        SceneManager.LoadScene(startScene);
        PlayerPrefs.DeleteAll();
    }
}
