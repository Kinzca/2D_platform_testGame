using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{

    public GameObject deathEffect;

    public GameObject collectible;
    [Range(1, 100)] public float changrToDrop;
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
        if(other.tag=="Enemy")
        {
            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathEffect,other.transform.position,other.transform.rotation);

            PlayerController.instance.Bounce();

            float dropSelect = Random.Range(1,100);
            if (changrToDrop <= dropSelect)
            {
                Instantiate(collectible,other.transform.position,other.transform.rotation);
                
            }

            SoundManager.instance.PlaySFX(3);
        }
        
    }
}
