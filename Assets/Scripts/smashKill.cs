using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smashKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && (PlayerController.instance.transform.position.y < transform.position.y))  
        {
            LevelController.instance.RespawnPlayerCo();

            SoundManager.instance.PlaySFX(8);
        }
    }
}
