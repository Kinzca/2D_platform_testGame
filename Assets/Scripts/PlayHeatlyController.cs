using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHeatlyController : MonoBehaviour
{
    public static PlayHeatlyController instance;

    public int currentHealth, maxHealth;

    public float invincibleLength;
    private float invincibleCount;

    private SpriteRenderer theSR;

    public GameObject deathEffect;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;
            if(invincibleCount <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invincibleCount<=0)
        {
            currentHealth--;

            SoundManager.instance.PlaySFX(9);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //gameObject.SetActive(false);
                LevelController.instance.RespawnPlayerCo();

                Instantiate(deathEffect, transform.position, transform.rotation);
            }
            else
            {
                invincibleCount = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .7f);

                PlayerController.instance.KnockBack();
            }
            UIController.instance.UpdateHeartDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;

        SoundManager.instance.PlaySFX(7);

        if (currentHealth >maxHealth )
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHeartDisplay();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "platform")
        {
            transform.parent = null;
        }
    }
}
