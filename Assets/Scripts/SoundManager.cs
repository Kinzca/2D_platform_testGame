using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource[] soundEffect;

    public AudioSource bgm, levelEndMusic,bossBattle,gameComplete;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffect[soundToPlay].Stop();

        soundEffect[soundToPlay].pitch =Random.Range(.9f,1.1f); 

        soundEffect[soundToPlay].Play();
    }

    public void EndLevelVictory()
    {
            bgm.Stop();
            levelEndMusic.Play();
            
    }

    public void EndGame()
    {

    }
}
