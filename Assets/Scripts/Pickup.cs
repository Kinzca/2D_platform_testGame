using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem,isHealth;

    public bool isCollected;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")&&!isCollected )
        {
           if(isGem)
            {
                LevelController.instance.gemscollected++;

                isCollected = true;

                Destroy(gameObject);

                Instantiate(pickupEffect , transform.position, transform.rotation);

                UIController.instance.UpdateGemCount();


                SoundManager.instance.PlaySFX(6);
            }

           if(isHealth)
            {
                if(PlayHeatlyController .instance .currentHealth !=PlayHeatlyController .instance .maxHealth)
                {
                    PlayHeatlyController.instance.HealPlayer();

                    isCollected = true;

                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);

                    UIController.instance.UpdateHeartDisplay ();


                    SoundManager.instance.PlaySFX(7);
                }
            }
        }
    }
}
