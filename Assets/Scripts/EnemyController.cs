using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;

    public Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D theRB;

    public SpriteRenderer theSR;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private Animator  anim;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        rightPoint.parent = null;
        leftPoint.parent = null;

        moveCount = moveTime;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount  > 0)
        {
            moveCount  -= Time.deltaTime;

            if (movingRight)
            {
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;

                    theSR.flipX = false;
                }
            }
            else
            {
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;

                    theSR.flipX = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * .75f, waitTime * 1.25f);
                
            }
            /*if (anim.gameObject.activeSelf)
            {
                anim.SetBool("isMoving", true);
            }*/

            anim.SetBool("isMoving", true);

        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            theRB.velocity = new Vector2(0f, theRB.velocity.y);

            if (waitCount < 0)
            {
                moveCount = Random.Range(moveTime * .75f, moveTime  * 1.25f);
            }
            /*if (anim.gameObject.activeSelf)
            {
                anim.SetBool("isMoving", false);
            }*/

            anim.SetBool("isMoving", false);


        }


    }
}
