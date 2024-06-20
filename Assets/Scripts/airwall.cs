using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class airwall : MonoBehaviour
{

    public GameObject airWall;

    public GameObject bossTank;

    private bool hasExecuted = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BossTankController.instance.isDefend)
        {
            airWall.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&& !hasExecuted)
        {
            airWall.SetActive(true);
            BossTankController.instance.theBoss.transform.localScale=new Vector3(1f, 1f, 1f);

            SoundManager.instance.bgm.Stop();
            SoundManager.instance.bossBattle.Play();

            hasExecuted = true;
        }
    }
}
