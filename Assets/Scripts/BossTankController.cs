using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    public static BossTankController instance;
    public enum bossStates { shoting,hurt,moving,ended };
    public bossStates currentStates;

    public Transform theBoss;
    public Animator anim;

    [Header("Movement")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    private bool moveRight;
    public float roundSpeed;

    public GameObject mine;
    public Transform minePoint;
    public float mineTime;
    private float mineCounter;
    public SpriteRenderer theSR;

    [Header("Shoting")]
    public GameObject bullet;
    public Transform firePoint;

    public float timeBetweenShots;//¼ÆÊ±Æ÷
    private float shotCounter;

    [Header("Hurt")]
    public float hurtTime;
    private float hurtCounter;
    public GameObject hitBox;

    [Header("Health")]
    public float bossHealth;
    public GameObject explosion;
    public bool isDefend;
    public float shotSpeedUp;
    public float mindSpeedUp;


    private void Awake()
    {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

        theBoss.localScale = new Vector3(-1f, 1f, 1f);
        currentStates = bossStates.shoting;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStates)
        {
            case bossStates.shoting:

                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;

                    SoundManager.instance.PlaySFX(2);
                }

                break;

            case bossStates.hurt:
                
                if (hurtCounter > 0)
                {
                    hurtCounter -= Time.deltaTime;

                    SoundManager.instance.PlaySFX(0);

                    if (hurtCounter <= 0)
                    {
                        currentStates = bossStates.moving;


                        if (isDefend)
                        {
                            theBoss.gameObject.SetActive(false);

                            SoundManager.instance.bossBattle.Stop();

                            SoundManager.instance.gameComplete.Play();

                            Instantiate(explosion, theBoss.position, theBoss.rotation);

                            currentStates = bossStates.ended;
                        }
                    }
                }
                break;

            case bossStates.moving:

                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x>rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;

                        EndMovement();
                    }
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;

                        EndMovement();
                    }
                }
                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0)
                {
                    mineCounter = mineTime;
                    Instantiate(mine, minePoint.position, transform.rotation);
                    SoundManager.instance.PlaySFX(1);
                }


                break;
        }

#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.H))
        {
            TankHit();
        }
#endif
    }

    public void TankHit()
    {
        hurtCounter = hurtTime;
        currentStates = bossStates.hurt;

        anim.SetTrigger("Hit");

        MIne[] mines = FindObjectsOfType<MIne>();
        if (mines.Length > 0)
        {
            foreach(MIne foundMine in mines)
            {
                foundMine.Explosion();
            }
        }

        bossHealth--;
        if (bossHealth <= 0)
        {
            isDefend = true;
        }
        else
        {
            mineCounter /= mindSpeedUp;
            shotCounter /= shotSpeedUp;
        }
    }

    private void EndMovement()
    {
        currentStates = bossStates.shoting;
        shotCounter = 0f;

        anim.SetTrigger("StopMoving");

        hitBox.SetActive(true);
    }
}
